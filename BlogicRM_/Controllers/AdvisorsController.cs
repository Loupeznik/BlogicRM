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
        public async Task<IActionResult> Index(string Filter)
        {
            var advisors = _context.Advisor.AsQueryable();

            if (!String.IsNullOrEmpty(Filter))
            {
                advisors = advisors.Where(a => a.Name.Contains(Filter) || a.Surname.Contains(Filter));
                ViewData["Filter"] = Filter;
            }
            return View(await advisors.ToListAsync());
        }

        // GET: Advisors/Details/5
        public async Task<IActionResult> Details(int? id)
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

            Dictionary<int, string> contracts = FindContracts(id);
            ViewData["Contracts"] = contracts;

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
                _context.Add(advisor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(advisor);
        }

        // GET: Advisors/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
        public async Task<IActionResult> Edit(int id, [Bind("AdvisorID,Name,Surname,Email,BirthNumber,Age,Phone")] Advisor advisor)
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
        public async Task<IActionResult> Delete(int? id)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advisor = await _context.Advisor.FindAsync(id);
            _context.Advisor.Remove(advisor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvisorExists(int id)
        {
            return _context.Advisor.Any(e => e.AdvisorID == id);
        }

        private Dictionary<int, string> FindContracts(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var contracts = _context.contractAdvisor.Where(c => c.AdvisorID == id);
            Dictionary<int, string> contractDict = new Dictionary<int, string>();
            foreach (var contract in contracts)
            {
                var contractId = _context.Contract.Where(a => a.ContractID == contract.ContractID).First().ContractID;
                var contractEN = _context.Contract.Where(a => a.ContractID == contract.ContractID).First().EvidenceNumber;
                contractDict.Add(contractId, contractEN);
            }
            return contractDict;
        }
    }
}
