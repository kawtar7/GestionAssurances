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
    public class CouverturesController : Controller
    {
        private readonly AssurancesContext _context;

        public CouverturesController(AssurancesContext context)
        {
            _context = context;
        }

        // GET: Couvertures
        public async Task<IActionResult> Index()
        {
            var assurancesContext = _context.Couvertures.Include(c => c.IdFormuleNavigation).Include(c => c.IdGarantieNavigation);
            return View(await assurancesContext.ToListAsync());
        }

        // GET: Couvertures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Couvertures == null)
            {
                return NotFound();
            }

            var couverture = await _context.Couvertures
                .Include(c => c.IdFormuleNavigation)
                .Include(c => c.IdGarantieNavigation)
                .FirstOrDefaultAsync(m => m.IdFormule == id);
            if (couverture == null)
            {
                return NotFound();
            }

            return View(couverture);
        }

        // GET: Couvertures/Create
        public IActionResult Create()
        {
            ViewData["IdFormule"] = new SelectList(_context.Formules, "IdFormule", "IdFormule");
            ViewData["IdGarantie"] = new SelectList(_context.Garanties, "IdGarantie", "IdGarantie");
            return View();
        }

        // POST: Couvertures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFormule,IdGarantie,Plafond,Franchise")] Couverture couverture)
        {
            if (ModelState.IsValid)
            {
                _context.Add(couverture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdFormule"] = new SelectList(_context.Formules, "IdFormule", "IdFormule", couverture.IdFormule);
            ViewData["IdGarantie"] = new SelectList(_context.Garanties, "IdGarantie", "IdGarantie", couverture.IdGarantie);
            return View(couverture);
        }

        // GET: Couvertures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Couvertures == null)
            {
                return NotFound();
            }

            var couverture = await _context.Couvertures.FindAsync(id);
            if (couverture == null)
            {
                return NotFound();
            }
            ViewData["IdFormule"] = new SelectList(_context.Formules, "IdFormule", "IdFormule", couverture.IdFormule);
            ViewData["IdGarantie"] = new SelectList(_context.Garanties, "IdGarantie", "IdGarantie", couverture.IdGarantie);
            return View(couverture);
        }

        // POST: Couvertures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFormule,IdGarantie,Plafond,Franchise")] Couverture couverture)
        {
            if (id != couverture.IdFormule)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(couverture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CouvertureExists(couverture.IdFormule))
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
            ViewData["IdFormule"] = new SelectList(_context.Formules, "IdFormule", "IdFormule", couverture.IdFormule);
            ViewData["IdGarantie"] = new SelectList(_context.Garanties, "IdGarantie", "IdGarantie", couverture.IdGarantie);
            return View(couverture);
        }

        // GET: Couvertures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Couvertures == null)
            {
                return NotFound();
            }

            var couverture = await _context.Couvertures
                .Include(c => c.IdFormuleNavigation)
                .Include(c => c.IdGarantieNavigation)
                .FirstOrDefaultAsync(m => m.IdFormule == id);
            if (couverture == null)
            {
                return NotFound();
            }

            return View(couverture);
        }

        // POST: Couvertures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Couvertures == null)
            {
                return Problem("Entity set 'AssurancesContext.Couvertures'  is null.");
            }
            var couverture = await _context.Couvertures.FindAsync(id);
            if (couverture != null)
            {
                _context.Couvertures.Remove(couverture);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CouvertureExists(int id)
        {
          return (_context.Couvertures?.Any(e => e.IdFormule == id)).GetValueOrDefault();
        }
    }
}
