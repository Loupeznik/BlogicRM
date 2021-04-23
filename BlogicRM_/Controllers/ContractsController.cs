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
            Dictionary<int, string> advisors = FindAdvisors(id);
            if (contract == null)
            {
                return NotFound();
            }

            ViewData["Advisors"] = advisors;

            return View(contract);
        }

        // GET: Contracts/Create
        public IActionResult Create()
        {
            ViewData["AdministratorID"] = new SelectList(_context.Advisor, "AdvisorID", "FullName");
            ViewData["ClientID"] = new SelectList(_context.Client, "ClientID", "FullName");
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
                _context.Add(new ContractAdvisor { ContractID = contract.ContractID, AdvisorID = contract.AdministratorID });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdministratorID"] = new SelectList(_context.Advisor, "AdvisorID", "FullName", contract.AdministratorID);
            ViewData["ClientID"] = new SelectList(_context.Client, "ClientID", "FullName", contract.ClientID);
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
            ViewData["AdministratorID"] = new SelectList(_context.Advisor, "AdvisorID", "FullName", contract.AdministratorID);
            ViewData["ClientID"] = new SelectList(_context.Client, "ClientID", "FullName", contract.ClientID);
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
                if (!(_context.contractAdvisor.Any(ca => ca.ContractID == id && ca.AdvisorID == contract.AdministratorID)))
                {
                    _context.Add(new ContractAdvisor { ContractID = id, AdvisorID = contract.AdministratorID });
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdministratorID"] = new SelectList(_context.Advisor, "AdvisorID", "FullName", contract.AdministratorID);
            ViewData["ClientID"] = new SelectList(_context.Client, "ClientID", "FullName", contract.ClientID);
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
            var advisors = _context.contractAdvisor.Where(c => c.ContractID == id);
            foreach (var advisor in advisors)
            {
                _context.contractAdvisor.Remove(advisor);
            }
            await _context.SaveChangesAsync();
            _context.Contract.Remove(contract);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Contracts/Advisors/5
        public async Task<IActionResult> Advisors(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (TempData["message"] != null)
            {
                ViewBag.message = TempData["message"].ToString();
                TempData.Remove("message");
            }

            var contract = await _context.Contract.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            Dictionary<int, string> advisors = FindAdvisors(id);
            ViewData["Advisors"] = advisors;
            ViewData["AdvisorID"] = new SelectList(_context.Advisor, "AdvisorID", "FullName");
            //ViewData["AdministratorID"] = new SelectList(_context.Advisor, "AdvisorID", "FullName", contract.AdministratorID);
            return View(contract);
        }

        // POST: Contracts/Advisors/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAdvisor(int id, int advisorId)
        {
            if (ModelState.IsValid)
            {
                if (_context.contractAdvisor.Any(ca => ca.ContractID == id && ca.AdvisorID == advisorId))
                {
                    TempData["message"] = "Vybraný poradce již je k této smlouvě přiřazen";
                    return RedirectToAction("Advisors", new { id });
                }
                _context.Add(new ContractAdvisor { ContractID = id, AdvisorID = advisorId });
                await _context.SaveChangesAsync();
                return RedirectToAction("Advisors", new { id });
            }
            return RedirectToAction("Advisors", new { id });
        }

        // POST: Contracts/DeleteAdvisor/5?advisorId=1
        [HttpPost, ActionName("DeleteAdvisor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAdvisor(int id, int advisorId)
        {
            var record = await _context.contractAdvisor.Where(ca => ca.ContractID == id).Where(ca => ca.AdvisorID == advisorId).FirstAsync();
            if (_context.Contract.Any(c => c.AdministratorID == advisorId))
            {
                TempData["message"] = "Vybraného poradce nelze smazat, protože je správcem smlouvy";
                return RedirectToAction("Advisors", new { id });
            }
            _context.contractAdvisor.Remove(record);
            await _context.SaveChangesAsync();
            return RedirectToAction("Advisors", new { id });
        }

        private bool ContractExists(int id)
        {
            return _context.Contract.Any(e => e.ContractID == id);
        }

        private Dictionary<int, string> FindAdvisors(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var advisors = _context.contractAdvisor.Where(c => c.ContractID == id);
            Dictionary<int, string> advisorDict = new Dictionary<int, string>();
            foreach (var advisor in advisors)
            {
                var advisorName = _context.Advisor.Where(a => a.AdvisorID == advisor.AdvisorID).First().FullName;
                var advisorId = _context.Advisor.Where(a => a.AdvisorID == advisor.AdvisorID).First().AdvisorID;
                advisorDict.Add(advisorId, advisorName);
            }
            return advisorDict;
        }
    }
}
