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
    public class ExpertsController : Controller
    {
        private readonly AssurancesContext _context;

        public ExpertsController(AssurancesContext context)
        {
            _context = context;
        }

        // GET: Experts
        public async Task<IActionResult> Index()
        {
              return _context.Experts != null ? 
                          View(await _context.Experts.ToListAsync()) :
                          Problem("Entity set 'AssurancesContext.Experts'  is null.");
        }

        // GET: Experts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Experts == null)
            {
                return NotFound();
            }

            var expert = await _context.Experts
                .FirstOrDefaultAsync(m => m.IdExpert == id);
            if (expert == null)
            {
                return NotFound();
            }

            return View(expert);
        }

        // GET: Experts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Experts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdExpert,NomExpert,Telephone")] Expert expert)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expert);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expert);
        }

        // GET: Experts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Experts == null)
            {
                return NotFound();
            }

            var expert = await _context.Experts.FindAsync(id);
            if (expert == null)
            {
                return NotFound();
            }
            return View(expert);
        }

        // POST: Experts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdExpert,NomExpert,Telephone")] Expert expert)
        {
            if (id != expert.IdExpert)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expert);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpertExists(expert.IdExpert))
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
            return View(expert);
        }

        // GET: Experts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Experts == null)
            {
                return NotFound();
            }

            var expert = await _context.Experts
                .FirstOrDefaultAsync(m => m.IdExpert == id);
            if (expert == null)
            {
                return NotFound();
            }

            return View(expert);
        }

        // POST: Experts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Experts == null)
            {
                return Problem("Entity set 'AssurancesContext.Experts'  is null.");
            }
            var expert = await _context.Experts.FindAsync(id);
            if (expert != null)
            {
                _context.Experts.Remove(expert);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpertExists(int id)
        {
          return (_context.Experts?.Any(e => e.IdExpert == id)).GetValueOrDefault();
        }
    }
}
