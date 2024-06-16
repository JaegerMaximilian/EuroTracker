using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Euro24Tracker.Data;
using Euro24Tracker.Types;

namespace Euro24Tracker.Controllers
{
    public class SpielerController : Controller
    {
        private readonly Euro24TrackerContext _context;

        public SpielerController(Euro24TrackerContext context)
        {
            _context = context;
        }

        // GET: Spieler
        public async Task<IActionResult> Index()
        {
            var euro24TrackerContext = _context.Spieler.Include(s => s.Nation);
            return View(await euro24TrackerContext.ToListAsync());
        }

        // GET: Spieler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spieler = await _context.Spieler
                .Include(s => s.Nation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spieler == null)
            {
                return NotFound();
            }

            return View(spieler);
        }

        // GET: Spieler/Create
        public IActionResult Create()
        {
            ViewData["NationId"] = new SelectList(_context.Nationen, "Id", "Name");
            return View();
        }

        // POST: Spieler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Tore,NationId")] Spieler spieler)
        {
            if (ModelState.IsValid)
            {
                Nation nation = _context.Nationen.FirstOrDefault(e => e.Id == spieler.NationId);
                spieler.Nation = nation;
                spieler.NationId = nation.Id;
                _context.Add(spieler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NationId"] = new SelectList(_context.Nationen, "Id", "Name", spieler.NationId);
            return View(spieler);
        }

        // GET: Spieler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spieler = await _context.Spieler.FindAsync(id);
            if (spieler == null)
            {
                return NotFound();
            }
            ViewData["NationId"] = new SelectList(_context.Nationen, "Id", "Name", spieler.NationId);
            return View(spieler);
        }

        // POST: Spieler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Tore,NationId")] Spieler spieler)
        {
            if (id != spieler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Nation nation = _context.Nationen.FirstOrDefault(e => e.Id == spieler.NationId);
                    spieler.Nation = nation;
                    spieler.NationId = nation.Id;
                    _context.Update(spieler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpielerExists(spieler.Id))
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
            ViewData["NationId"] = new SelectList(_context.Nationen, "Id", "Name", spieler.NationId);
            return View(spieler);
        }

        // GET: Spieler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spieler = await _context.Spieler
                .Include(s => s.Nation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spieler == null)
            {
                return NotFound();
            }

            return View(spieler);
        }

        // POST: Spieler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spieler = await _context.Spieler.FindAsync(id);
            if (spieler != null)
            {
                _context.Spieler.Remove(spieler);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpielerExists(int id)
        {
            return _context.Spieler.Any(e => e.Id == id);
        }
    }
}
