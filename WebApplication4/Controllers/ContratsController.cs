using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class ContratsController : Controller
    {
        private readonly AssurancesContext _context;

        public ContratsController(AssurancesContext context)
        {
            _context = context;
        }

        // GET: Contrats
        public async Task<IActionResult> Index()
        {
            var assurancesContext = _context.Contrats.Include(c => c.IdClientNavigation).Include(c => c.IdFormuleNavigation);
            return View(await assurancesContext.ToListAsync());
        }

        // GET: Contrats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contrats == null)
            {
                return NotFound();
            }

            var contrat = await _context.Contrats
                .Include(c => c.IdClientNavigation)
                .Include(c => c.IdFormuleNavigation)
                .FirstOrDefaultAsync(m => m.IdContrat == id);
            if (contrat == null)
            {
                return NotFound();
            }

            return View(contrat);
        }

        // GET: Contrats/Create
        public IActionResult Create()
        {
            ViewData["IdClient"] = new SelectList(_context.Clients, "IdClient", "IdClient");
            ViewData["IdFormule"] = new SelectList(_context.Formules, "IdFormule", "IdFormule");
            return View();
        }

        // POST: Contrats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdContrat,DateSouscription,DateEcheance,IdClient,IdFormule")] Contrat contrat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contrat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdClient"] = new SelectList(_context.Clients, "IdClient", "IdClient", contrat.IdClient);
            ViewData["IdFormule"] = new SelectList(_context.Formules, "IdFormule", "IdFormule", contrat.IdFormule);
            return View(contrat);
        }

        // GET: Contrats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Contrats == null)
            {
                return NotFound();
            }

            var contrat = await _context.Contrats.FindAsync(id);
            if (contrat == null)
            {
                return NotFound();
            }
            ViewData["IdClient"] = new SelectList(_context.Clients, "IdClient", "IdClient", contrat.IdClient);
            ViewData["IdFormule"] = new SelectList(_context.Formules, "IdFormule", "IdFormule", contrat.IdFormule);
            return View(contrat);
        }

        // POST: Contrats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdContrat,DateSouscription,DateEcheance,IdClient,IdFormule")] Contrat contrat)
        {
            if (id != contrat.IdContrat)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contrat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContratExists(contrat.IdContrat))
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
            ViewData["IdClient"] = new SelectList(_context.Clients, "IdClient", "IdClient", contrat.IdClient);
            ViewData["IdFormule"] = new SelectList(_context.Formules, "IdFormule", "IdFormule", contrat.IdFormule);
            return View(contrat);
        }

        // GET: Contrats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Contrats == null)
            {
                return NotFound();
            }

            var contrat = await _context.Contrats
                .Include(c => c.IdClientNavigation)
                .Include(c => c.IdFormuleNavigation)
                .FirstOrDefaultAsync(m => m.IdContrat == id);
            if (contrat == null)
            {
                return NotFound();
            }

            return View(contrat);
        }

        // POST: Contrats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contrats == null)
            {
                return Problem("Entity set 'AssurancesContext.Contrats'  is null.");
            }
            var contrat = await _context.Contrats.FindAsync(id);
            if (contrat != null)
            {
                _context.Contrats.Remove(contrat);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContratExists(int id)
        {
          return (_context.Contrats?.Any(e => e.IdContrat == id)).GetValueOrDefault();
        }
    }
}
