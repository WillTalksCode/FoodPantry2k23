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
    public class PeopleController : Controller
    {
        private readonly FPContext _context;

        public PeopleController(FPContext context)
        {
            _context = context;
        }

        // GET: People
        [Authorize]
        public async Task<IActionResult> Index(string FirstOrLastName = "")
        {
            if (!string.IsNullOrWhiteSpace(FirstOrLastName))
            {
                return _context.People != null ?
                            View(await _context.People.Where(x => (!string.IsNullOrEmpty(x.FirstName) && x.FirstName.Contains(FirstOrLastName)) || (!string.IsNullOrEmpty(x.LastName) && x.LastName.Contains(FirstOrLastName))).ToListAsync()) : Problem("Entity set 'FPContext.People'  is null.");
            }
            else
            {
                return _context.People != null ?
                            View(await _context.People.ToListAsync()) :
                            Problem("Entity set 'FPContext.People'  is null.");
            }
        }

        // GET: People/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.People == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .FirstOrDefaultAsync(m => m.id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,FirstName,MiddleName,LastName,DOB,Gender,HouseHoldID,Email,Phone")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.People == null)
            {
                return NotFound();
            }

            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,FirstName,MiddleName,LastName,DOB,Gender,HouseHoldID,Email,Phone")] Person person)
        {
            if (id != person.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.id))
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
            return View(person);
        }

        // GET: People/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.People == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .FirstOrDefaultAsync(m => m.id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.People == null)
            {
                return Problem("Entity set 'FPContext.People'  is null.");
            }
            var person = await _context.People.FindAsync(id);
            if (person != null)
            {
                _context.People.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        private bool PersonExists(int id)
        {
            return (_context.People?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
