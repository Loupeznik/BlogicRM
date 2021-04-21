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
    public class ContractsController : Controller
    {
        private readonly BlogicRM _context;

        public ContractsController(BlogicRM context)
        {
            _context = context;
        }

        // GET: Contracts
        public async Task<IActionResult> Index()
        {
            var blogicRM = _context.Contract.Include(c => c.Administrator).Include(c => c.Client).Include(c => c.Institution);
            return View(await blogicRM.ToListAsync());
        }

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contract
                .Include(c => c.Administrator)
                .Include(c => c.Client)
                .Include(c => c.Institution)
                .FirstOrDefaultAsync(m => m.ContractID == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // GET: Contracts/Create
        public IActionResult Create()
        {
            ViewData["AdministratorID"] = new SelectList(_context.Advisor, "AdvisorID", "BirthNumber");
            ViewData["ClientID"] = new SelectList(_context.Client, "ClientID", "BirthNumber");
            ViewData["InstitutionID"] = new SelectList(_context.Institution, "InstitutionID", "Name");
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContractID,EvidenceNumber,InstitutionID,ClientID,AdministratorID,ConclusionDate,ValidityDate,EndDate")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contract);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdministratorID"] = new SelectList(_context.Advisor, "AdvisorID", "BirthNumber", contract.AdministratorID);
            ViewData["ClientID"] = new SelectList(_context.Client, "ClientID", "BirthNumber", contract.ClientID);
            ViewData["InstitutionID"] = new SelectList(_context.Institution, "InstitutionID", "Name", contract.InstitutionID);
            return View(contract);
        }

        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contract.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            ViewData["AdministratorID"] = new SelectList(_context.Advisor, "AdvisorID", "BirthNumber", contract.AdministratorID);
            ViewData["ClientID"] = new SelectList(_context.Client, "ClientID", "BirthNumber", contract.ClientID);
            ViewData["InstitutionID"] = new SelectList(_context.Institution, "InstitutionID", "Name", contract.InstitutionID);
            return View(contract);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContractID,EvidenceNumber,InstitutionID,ClientID,AdministratorID,ConclusionDate,ValidityDate,EndDate")] Contract contract)
        {
            if (id != contract.ContractID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contract);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractExists(contract.ContractID))
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
            ViewData["AdministratorID"] = new SelectList(_context.Advisor, "AdvisorID", "BirthNumber", contract.AdministratorID);
            ViewData["ClientID"] = new SelectList(_context.Client, "ClientID", "BirthNumber", contract.ClientID);
            ViewData["InstitutionID"] = new SelectList(_context.Institution, "InstitutionID", "Name", contract.InstitutionID);
            return View(contract);
        }

        // GET: Contracts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contract
                .Include(c => c.Administrator)
                .Include(c => c.Client)
                .Include(c => c.Institution)
                .FirstOrDefaultAsync(m => m.ContractID == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contract = await _context.Contract.FindAsync(id);
            _context.Contract.Remove(contract);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractExists(int id)
        {
            return _context.Contract.Any(e => e.ContractID == id);
        }
    }
}
