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
    public class DossiersSinistresController : Controller
    {
        private readonly AssurancesContext _context;

        public DossiersSinistresController(AssurancesContext context)
        {
            _context = context;
        }

        // GET: DossiersSinistres
        public async Task<IActionResult> Index()
        {
            var assurancesContext = _context.DossiersSinistres.Include(d => d.IdContratNavigation).Include(d => d.IdCorrespondantNavigation).Include(d => d.IdExpertNavigation);
            return View(await assurancesContext.ToListAsync());
        }

        // GET: DossiersSinistres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DossiersSinistres == null)
            {
                return NotFound();
            }

            var dossiersSinistre = await _context.DossiersSinistres
                .Include(d => d.IdContratNavigation)
                .Include(d => d.IdCorrespondantNavigation)
                .Include(d => d.IdExpertNavigation)
                .FirstOrDefaultAsync(m => m.IdDossierSinistre == id);
            if (dossiersSinistre == null)
            {
                return NotFound();
            }

            return View(dossiersSinistre);
        }

        // GET: DossiersSinistres/Create
        public IActionResult Create()
        {
            ViewData["IdContrat"] = new SelectList(_context.Contrats, "IdContrat", "IdContrat");
            ViewData["IdCorrespondant"] = new SelectList(_context.Correspondants, "IdCorrespondant", "IdCorrespondant");
            ViewData["IdExpert"] = new SelectList(_context.Experts, "IdExpert", "IdExpert");
            return View();
        }

        // POST: DossiersSinistres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDossierSinistre,DateCouverture,DateCloture,Indemnites,IdCorrespondant,IdExpert,IdContrat")] DossiersSinistre dossiersSinistre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dossiersSinistre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdContrat"] = new SelectList(_context.Contrats, "IdContrat", "IdContrat", dossiersSinistre.IdContrat);
            ViewData["IdCorrespondant"] = new SelectList(_context.Correspondants, "IdCorrespondant", "IdCorrespondant", dossiersSinistre.IdCorrespondant);
            ViewData["IdExpert"] = new SelectList(_context.Experts, "IdExpert", "IdExpert", dossiersSinistre.IdExpert);
            return View(dossiersSinistre);
        }

        // GET: DossiersSinistres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DossiersSinistres == null)
            {
                return NotFound();
            }

            var dossiersSinistre = await _context.DossiersSinistres.FindAsync(id);
            if (dossiersSinistre == null)
            {
                return NotFound();
            }
            ViewData["IdContrat"] = new SelectList(_context.Contrats, "IdContrat", "IdContrat", dossiersSinistre.IdContrat);
            ViewData["IdCorrespondant"] = new SelectList(_context.Correspondants, "IdCorrespondant", "IdCorrespondant", dossiersSinistre.IdCorrespondant);
            ViewData["IdExpert"] = new SelectList(_context.Experts, "IdExpert", "IdExpert", dossiersSinistre.IdExpert);
            return View(dossiersSinistre);
        }

        // POST: DossiersSinistres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDossierSinistre,DateCouverture,DateCloture,Indemnites,IdCorrespondant,IdExpert,IdContrat")] DossiersSinistre dossiersSinistre)
        {
            if (id != dossiersSinistre.IdDossierSinistre)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dossiersSinistre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DossiersSinistreExists(dossiersSinistre.IdDossierSinistre))
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
            ViewData["IdContrat"] = new SelectList(_context.Contrats, "IdContrat", "IdContrat", dossiersSinistre.IdContrat);
            ViewData["IdCorrespondant"] = new SelectList(_context.Correspondants, "IdCorrespondant", "IdCorrespondant", dossiersSinistre.IdCorrespondant);
            ViewData["IdExpert"] = new SelectList(_context.Experts, "IdExpert", "IdExpert", dossiersSinistre.IdExpert);
            return View(dossiersSinistre);
        }

        // GET: DossiersSinistres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DossiersSinistres == null)
            {
                return NotFound();
            }

            var dossiersSinistre = await _context.DossiersSinistres
                .Include(d => d.IdContratNavigation)
                .Include(d => d.IdCorrespondantNavigation)
                .Include(d => d.IdExpertNavigation)
                .FirstOrDefaultAsync(m => m.IdDossierSinistre == id);
            if (dossiersSinistre == null)
            {
                return NotFound();
            }

            return View(dossiersSinistre);
        }

        // POST: DossiersSinistres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DossiersSinistres == null)
            {
                return Problem("Entity set 'AssurancesContext.DossiersSinistres'  is null.");
            }
            var dossiersSinistre = await _context.DossiersSinistres.FindAsync(id);
            if (dossiersSinistre != null)
            {
                _context.DossiersSinistres.Remove(dossiersSinistre);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DossiersSinistreExists(int id)
        {
          return (_context.DossiersSinistres?.Any(e => e.IdDossierSinistre == id)).GetValueOrDefault();
        }
    }
}
