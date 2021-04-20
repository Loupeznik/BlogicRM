using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogicRM_.Data;
using BlogicRM_.Models;

namespace BlogicRM_.Controllers
{
    public class AdvisorsController : Controller
    {
        private readonly BlogicRM _context;

        public AdvisorsController(BlogicRM context)
        {
            _context = context;
        }

        // GET: Advisors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Advisor.ToListAsync());
        }

        // GET: Advisors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advisor = await _context.Advisor
                .FirstOrDefaultAsync(m => m.AdvisorID == id);
            if (advisor == null)
            {
                return NotFound();
            }

            return View(advisor);
        }

        // GET: Advisors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Advisors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdvisorID,Name,Surname,Email,BirthNumber,Age,Phone")] Advisor advisor)
        {
            if (ModelState.IsValid)
            {
                advisor.AdvisorID = Guid.NewGuid();
                _context.Add(advisor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(advisor);
        }

        // GET: Advisors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advisor = await _context.Advisor.FindAsync(id);
            if (advisor == null)
            {
                return NotFound();
            }
            return View(advisor);
        }

        // POST: Advisors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AdvisorID,Name,Surname,Email,BirthNumber,Age,Phone")] Advisor advisor)
        {
            if (id != advisor.AdvisorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advisor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvisorExists(advisor.AdvisorID))
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
            return View(advisor);
        }

        // GET: Advisors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advisor = await _context.Advisor
                .FirstOrDefaultAsync(m => m.AdvisorID == id);
            if (advisor == null)
            {
                return NotFound();
            }

            return View(advisor);
        }

        // POST: Advisors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var advisor = await _context.Advisor.FindAsync(id);
            _context.Advisor.Remove(advisor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvisorExists(Guid id)
        {
            return _context.Advisor.Any(e => e.AdvisorID == id);
        }
    }
}
