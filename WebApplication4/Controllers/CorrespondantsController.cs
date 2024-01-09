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
    public class CorrespondantsController : Controller
    {
        private readonly AssurancesContext _context;

        public CorrespondantsController(AssurancesContext context)
        {
            _context = context;
        }

        // GET: Correspondants
        public async Task<IActionResult> Index()
        {
              return _context.Correspondants != null ? 
                          View(await _context.Correspondants.ToListAsync()) :
                          Problem("Entity set 'AssurancesContext.Correspondants'  is null.");
        }

        // GET: Correspondants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Correspondants == null)
            {
                return NotFound();
            }

            var correspondant = await _context.Correspondants
                .FirstOrDefaultAsync(m => m.IdCorrespondant == id);
            if (correspondant == null)
            {
                return NotFound();
            }

            return View(correspondant);
        }

        // GET: Correspondants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Correspondants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCorrespondant,NomCorrespondant,Telephone")] Correspondant correspondant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(correspondant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(correspondant);
        }

        // GET: Correspondants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Correspondants == null)
            {
                return NotFound();
            }

            var correspondant = await _context.Correspondants.FindAsync(id);
            if (correspondant == null)
            {
                return NotFound();
            }
            return View(correspondant);
        }

        // POST: Correspondants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCorrespondant,NomCorrespondant,Telephone")] Correspondant correspondant)
        {
            if (id != correspondant.IdCorrespondant)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(correspondant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CorrespondantExists(correspondant.IdCorrespondant))
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
            return View(correspondant);
        }

        // GET: Correspondants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Correspondants == null)
            {
                return NotFound();
            }

            var correspondant = await _context.Correspondants
                .FirstOrDefaultAsync(m => m.IdCorrespondant == id);
            if (correspondant == null)
            {
                return NotFound();
            }

            return View(correspondant);
        }

        // POST: Correspondants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Correspondants == null)
            {
                return Problem("Entity set 'AssurancesContext.Correspondants'  is null.");
            }
            var correspondant = await _context.Correspondants.FindAsync(id);
            if (correspondant != null)
            {
                _context.Correspondants.Remove(correspondant);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CorrespondantExists(int id)
        {
          return (_context.Correspondants?.Any(e => e.IdCorrespondant == id)).GetValueOrDefault();
        }
    }
}
