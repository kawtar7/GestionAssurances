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
    public class InterventionsController : Controller
    {
        private readonly AssurancesContext _context;

        public InterventionsController(AssurancesContext context)
        {
            _context = context;
        }

        // GET: Interventions
        public async Task<IActionResult> Index()
        {
            var assurancesContext = _context.Interventions.Include(i => i.IdDossierInterventionNavigation);
            return View(await assurancesContext.ToListAsync());
        }

        // GET: Interventions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Interventions == null)
            {
                return NotFound();
            }

            var intervention = await _context.Interventions
                .Include(i => i.IdDossierInterventionNavigation)
                .FirstOrDefaultAsync(m => m.IdDossierIntervention == id);
            if (intervention == null)
            {
                return NotFound();
            }

            return View(intervention);
        }

        // GET: Interventions/Create
        public IActionResult Create()
        {
            ViewData["IdDossierIntervention"] = new SelectList(_context.DossiersSinistres, "IdDossierSinistre", "IdDossierSinistre");
            return View();
        }

        // POST: Interventions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDossierIntervention,DateIntervention")] Intervention intervention)
        {
            if (ModelState.IsValid)
            {
                _context.Add(intervention);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDossierIntervention"] = new SelectList(_context.DossiersSinistres, "IdDossierSinistre", "IdDossierSinistre", intervention.IdDossierIntervention);
            return View(intervention);
        }

        // GET: Interventions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Interventions == null)
            {
                return NotFound();
            }

            var intervention = await _context.Interventions.FindAsync(id);
            if (intervention == null)
            {
                return NotFound();
            }
            ViewData["IdDossierIntervention"] = new SelectList(_context.DossiersSinistres, "IdDossierSinistre", "IdDossierSinistre", intervention.IdDossierIntervention);
            return View(intervention);
        }

        // POST: Interventions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDossierIntervention,DateIntervention")] Intervention intervention)
        {
            if (id != intervention.IdDossierIntervention)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(intervention);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterventionExists(intervention.IdDossierIntervention))
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
            ViewData["IdDossierIntervention"] = new SelectList(_context.DossiersSinistres, "IdDossierSinistre", "IdDossierSinistre", intervention.IdDossierIntervention);
            return View(intervention);
        }

        // GET: Interventions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Interventions == null)
            {
                return NotFound();
            }

            var intervention = await _context.Interventions
                .Include(i => i.IdDossierInterventionNavigation)
                .FirstOrDefaultAsync(m => m.IdDossierIntervention == id);
            if (intervention == null)
            {
                return NotFound();
            }

            return View(intervention);
        }

        // POST: Interventions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Interventions == null)
            {
                return Problem("Entity set 'AssurancesContext.Interventions'  is null.");
            }
            var intervention = await _context.Interventions.FindAsync(id);
            if (intervention != null)
            {
                _context.Interventions.Remove(intervention);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InterventionExists(int id)
        {
          return (_context.Interventions?.Any(e => e.IdDossierIntervention == id)).GetValueOrDefault();
        }
    }
}
