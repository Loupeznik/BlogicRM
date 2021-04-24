using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogicRM_.Data;
using BlogicRM_.Models;
using System.Text;

namespace BlogicRM_.Controllers
{
    public class ClientsController : Controller
    {
        private readonly BlogicRM _context;

        public ClientsController(BlogicRM context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index(string Filter)
        {
            var clients = _context.Client.AsQueryable();

            if (!String.IsNullOrEmpty(Filter))
            {
                clients = clients.Where(c => c.Name.Contains(Filter) || c.Surname.Contains(Filter));
                ViewData["Filter"] = Filter;
            }
            return View(await clients.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .Include(c => c.Contracts)
                .FirstOrDefaultAsync(m => m.ClientID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientID,Name,Surname,Email,BirthNumber,Age,Phone")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientID,Name,Surname,Email,BirthNumber,Age,Phone")] Client client)
        {
            if (id != client.ClientID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientID))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.ClientID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Client.FindAsync(id);
            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ExportToCSV()
        {
            var data = await _context.Client.ToListAsync();

            try
            {
                StringBuilder sb = new();
                sb.AppendLine("ID;Jméno;Příjmení;Email;Rodné číslo;Věk;Telefon");
                foreach (var c in data)
                {
                    sb.AppendLine(
                        $"{c.ClientID};" +
                        $"{c.Name};" +
                        $"{c.Surname};" +
                        $"{c.Email};" +
                        $"{c.BirthNumber};" +
                        $"{c.Age};" +
                        $"{c.Phone}"
                        );
                }
                return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "BlogicRM_klienti_export.csv");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        private bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.ClientID == id);
        }
    }
}
