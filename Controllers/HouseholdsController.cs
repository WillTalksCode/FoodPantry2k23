using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodPantry2k23;
using FoodPantry2k23.Models;
using Microsoft.AspNetCore.Authorization;

namespace FoodPantry2k23.Controllers
{
    public class HouseholdsController : Controller
    {
        private readonly FPContext _context;
        public HouseholdsController(FPContext context)
        {
            _context = context;
        }

        // GET: Households
        [Authorize]
        public async Task<IActionResult> Index()
        {
              return _context.Households != null ? 
                          View(await _context.Households.ToListAsync()) :
                          Problem("Entity set 'FPContext.Households'  is null.");
        }

        // GET: Households/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Households == null)
            {
                return NotFound();
            }

            var household = await _context.Households
                .FirstOrDefaultAsync(m => m.HouseHoldID == id);
            if (household == null)
            {
                return NotFound();
            }

            return View(household);
        }

        // GET: Households/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Households/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HouseHoldID,Address1,Address2,City,StateProvince,ZipCode,ConsentFormOnFile,VerbalConsentGiven,ConsentFormSigned,VerbalConsentGivenOn,AdminNotes")] Household household)
        {
            if (ModelState.IsValid)
            {
                _context.Add(household);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(household);
        }

        // GET: Households/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Households == null)
            {
                return NotFound();
            }

            var household = await _context.Households.FindAsync(id);
            if (household == null)
            {
                return NotFound();
            }
            return View(household);
        }

        // POST: Households/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HouseHoldID,Address1,Address2,City,StateProvince,ZipCode,ConsentFormOnFile,VerbalConsentGiven,ConsentFormSigned,VerbalConsentGivenOn,AdminNotes")] Household household)
        {
            if (id != household.HouseHoldID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(household);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HouseholdExists(household.HouseHoldID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(household);
        }

        // GET: Households/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Households == null)
            {
                return NotFound();
            }

            var household = await _context.Households
                .FirstOrDefaultAsync(m => m.HouseHoldID == id);
            if (household == null)
            {
                return NotFound();
            }

            return View(household);
        }

        // POST: Households/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Households == null)
            {
                return Problem("Entity set 'FPContext.Households'  is null.");
            }
            var household = await _context.Households.FindAsync(id);
            if (household != null)
            {
                _context.Households.Remove(household);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        private bool HouseholdExists(int id)
        {
          return (_context.Households?.Any(e => e.HouseHoldID == id)).GetValueOrDefault();
        }
    }
}
