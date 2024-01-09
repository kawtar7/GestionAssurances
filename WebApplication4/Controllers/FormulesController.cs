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
    public class FormulesController : Controller
    {
        private readonly AssurancesContext _context;

        public FormulesController(AssurancesContext context)
        {
            _context = context;
        }

        // GET: Formules
        public async Task<IActionResult> Index()
        {
              return _context.Formules != null ? 
                          View(await _context.Formules.ToListAsync()) :
                          Problem("Entity set 'AssurancesContext.Formules'  is null.");
        }

        // GET: Formules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Formules == null)
            {
                return NotFound();
            }

            var formule = await _context.Formules
                .FirstOrDefaultAsync(m => m.IdFormule == id);
            if (formule == null)
            {
                return NotFound();
            }

            return View(formule);
        }

        // GET: Formules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Formules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFormule,Libelle")] Formule formule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formule);
        }

        // GET: Formules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Formules == null)
            {
                return NotFound();
            }

            var formule = await _context.Formules.FindAsync(id);
            if (formule == null)
            {
                return NotFound();
            }
            return View(formule);
        }

        // POST: Formules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFormule,Libelle")] Formule formule)
        {
            if (id != formule.IdFormule)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormuleExists(formule.IdFormule))
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
            return View(formule);
        }

        // GET: Formules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Formules == null)
            {
                return NotFound();
            }

            var formule = await _context.Formules
                .FirstOrDefaultAsync(m => m.IdFormule == id);
            if (formule == null)
            {
                return NotFound();
            }

            return View(formule);
        }

        // POST: Formules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Formules == null)
            {
                return Problem("Entity set 'AssurancesContext.Formules'  is null.");
            }
            var formule = await _context.Formules.FindAsync(id);
            if (formule != null)
            {
                _context.Formules.Remove(formule);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormuleExists(int id)
        {
          return (_context.Formules?.Any(e => e.IdFormule == id)).GetValueOrDefault();
        }
    }
}
