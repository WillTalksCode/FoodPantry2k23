using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodPantry2k23;
using FoodPantry2k23.Models;

namespace FoodPantry2k23.Controllers
{
    public class ReferralsController : Controller
    {
        private readonly FPContext _context;

        public ReferralsController(FPContext context)
        {
            _context = context;
        }

        // GET: Referrals
        public async Task<IActionResult> Index()
        {
              return _context.Referrals != null ? 
                          View(await _context.Referrals.ToListAsync()) :
                          Problem("Entity set 'FPContext.Referrals'  is null.");
        }

        // GET: Referrals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Referrals == null)
            {
                return NotFound();
            }

            var referral = await _context.Referrals
                .FirstOrDefaultAsync(m => m.ReferralID == id);
            if (referral == null)
            {
                return NotFound();
            }

            return View(referral);
        }

        // GET: Referrals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Referrals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReferralID,HouseHoldID,Service,ReferralDate")] Referral referral)
        {
            if (ModelState.IsValid)
            {
                _context.Add(referral);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(referral);
        }

        // GET: Referrals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Referrals == null)
            {
                return NotFound();
            }

            var referral = await _context.Referrals.FindAsync(id);
            if (referral == null)
            {
                return NotFound();
            }
            return View(referral);
        }

        // POST: Referrals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReferralID,HouseHoldID,Service,ReferralDate")] Referral referral)
        {
            if (id != referral.ReferralID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(referral);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReferralExists(referral.ReferralID))
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
            return View(referral);
        }

        // GET: Referrals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Referrals == null)
            {
                return NotFound();
            }

            var referral = await _context.Referrals
                .FirstOrDefaultAsync(m => m.ReferralID == id);
            if (referral == null)
            {
                return NotFound();
            }

            return View(referral);
        }

        // POST: Referrals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Referrals == null)
            {
                return Problem("Entity set 'FPContext.Referrals'  is null.");
            }
            var referral = await _context.Referrals.FindAsync(id);
            if (referral != null)
            {
                _context.Referrals.Remove(referral);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReferralExists(int id)
        {
          return (_context.Referrals?.Any(e => e.ReferralID == id)).GetValueOrDefault();
        }
    }
}
