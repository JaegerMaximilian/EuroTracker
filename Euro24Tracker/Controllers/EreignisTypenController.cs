using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Euro24Tracker.Data;
using EURO2024App.Types;

namespace Euro24Tracker.Controllers
{
    public class EreignisTypenController : Controller
    {
        private readonly Euro24TrackerContext _context;

        public EreignisTypenController(Euro24TrackerContext context)
        {
            _context = context;
        }

        // GET: EreignisTypen
        public async Task<IActionResult> Index()
        {
            return View(await _context.EreignisTyp.ToListAsync());
        }

        // GET: EreignisTypen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ereignisTyp = await _context.EreignisTyp
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ereignisTyp == null)
            {
                return NotFound();
            }

            return View(ereignisTyp);
        }

        // GET: EreignisTypen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EreignisTypen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImageLink")] EreignisTyp ereignisTyp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ereignisTyp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ereignisTyp);
        }

        // GET: EreignisTypen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ereignisTyp = await _context.EreignisTyp.FindAsync(id);
            if (ereignisTyp == null)
            {
                return NotFound();
            }
            return View(ereignisTyp);
        }

        // POST: EreignisTypen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImageLink")] EreignisTyp ereignisTyp)
        {
            if (id != ereignisTyp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ereignisTyp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EreignisTypExists(ereignisTyp.Id))
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
            return View(ereignisTyp);
        }

        // GET: EreignisTypen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ereignisTyp = await _context.EreignisTyp
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ereignisTyp == null)
            {
                return NotFound();
            }

            return View(ereignisTyp);
        }

        // POST: EreignisTypen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ereignisTyp = await _context.EreignisTyp.FindAsync(id);
            if (ereignisTyp != null)
            {
                _context.EreignisTyp.Remove(ereignisTyp);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EreignisTypExists(int id)
        {
            return _context.EreignisTyp.Any(e => e.Id == id);
        }
    }
}
