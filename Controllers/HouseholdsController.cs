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
            
            household.HouseHoldMembers = await (from x in _context.People where x.HouseHoldID == id select x).ToListAsync();
            return View(household);
        }

        [Authorize]
        public async Task<IActionResult> AddMemberToHousehold(int? HouseHoldId, int? PersonId)
        {
            if (HouseHoldId == null || _context.Households == null || PersonId == null)
            {
                return NotFound();
            }

            var household = await _context.Households.FindAsync(HouseHoldId);
            var person = await _context.People.FindAsync(PersonId);
            if (household == null || person == null)
            {
                return NotFound();
            }
            person.HouseHoldID = household.HouseHoldID;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HouseholdExists(household.HouseHoldID) || !PersonExists(person.id))
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
            return View(nameof(Details));
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

        [Authorize]
        private bool PersonExists(int id)
        {
            return (_context.People?.Any(e => e.id == id)).GetValueOrDefault();
        }

        [Authorize]
        public PartialViewResult SearchPeopleForHousehold(string SearchName = "")
        {
            List<Person> SearchResults = new List<Person>();
            var isAjax = this.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (!string.IsNullOrWhiteSpace(SearchName) && isAjax)
            {
                SearchResults = _context.People.Where(x => (!string.IsNullOrEmpty(x.FirstName) && x.FirstName.StartsWith(SearchName)) || (!string.IsNullOrEmpty(x.LastName) && x.LastName.StartsWith(SearchName))).ToList<Person>();
            }
            else
            {
                SearchResults = _context.People.Take(10).ToList<Person>();
            }
            return PartialView("_AddNewHouseHoldMemberModal", SearchResults);
        }

        [HttpPost]
        public List<Person> GetSearchResults(string SearchName = "")
        {
            if (!string.IsNullOrWhiteSpace(SearchName))
            {
                return _context.People.Where(x => (!string.IsNullOrEmpty(x.FirstName) && x.FirstName.StartsWith(SearchName)) || (!string.IsNullOrEmpty(x.LastName) && x.LastName.StartsWith(SearchName))).ToList<Person>();
            }
            else
                return _context.People.ToList<Person>();
        }


    }
}
