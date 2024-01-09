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
    public class GarantiesController : Controller
    {
        private readonly AssurancesContext _context;

        public GarantiesController(AssurancesContext context)
        {
            _context = context;
        }

        // GET: Garanties
        public async Task<IActionResult> Index()
        {
              return _context.Garanties != null ? 
                          View(await _context.Garanties.ToListAsync()) :
                          Problem("Entity set 'AssurancesContext.Garanties'  is null.");
        }

        // GET: Garanties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Garanties == null)
            {
                return NotFound();
            }

            var garanty = await _context.Garanties
                .FirstOrDefaultAsync(m => m.IdGarantie == id);
            if (garanty == null)
            {
                return NotFound();
            }

            return View(garanty);
        }

        // GET: Garanties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Garanties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdGarantie,Libelle")] Garanty garanty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(garanty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(garanty);
        }

        // GET: Garanties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Garanties == null)
            {
                return NotFound();
            }

            var garanty = await _context.Garanties.FindAsync(id);
            if (garanty == null)
            {
                return NotFound();
            }
            return View(garanty);
        }

        // POST: Garanties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdGarantie,Libelle")] Garanty garanty)
        {
            if (id != garanty.IdGarantie)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(garanty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GarantyExists(garanty.IdGarantie))
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
            return View(garanty);
        }

        // GET: Garanties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Garanties == null)
            {
                return NotFound();
            }

            var garanty = await _context.Garanties
                .FirstOrDefaultAsync(m => m.IdGarantie == id);
            if (garanty == null)
            {
                return NotFound();
            }

            return View(garanty);
        }

        // POST: Garanties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Garanties == null)
            {
                return Problem("Entity set 'AssurancesContext.Garanties'  is null.");
            }
            var garanty = await _context.Garanties.FindAsync(id);
            if (garanty != null)
            {
                _context.Garanties.Remove(garanty);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GarantyExists(int id)
        {
          return (_context.Garanties?.Any(e => e.IdGarantie == id)).GetValueOrDefault();
        }
    }
}
