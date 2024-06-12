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
    public class GruppenController : Controller
    {
        private readonly Euro24TrackerContext _context;

        public GruppenController(Euro24TrackerContext context)
        {
            _context = context;
        }

        // GET: Gruppen
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gruppen.ToListAsync());
        }

        // GET: Gruppen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gruppe = await _context.Gruppen
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gruppe == null)
            {
                return NotFound();
            }

            return View(gruppe);
        }

        // GET: Gruppen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gruppen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Gruppe gruppe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gruppe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gruppe);
        }

        // GET: Gruppen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gruppe = await _context.Gruppen.FindAsync(id);
            if (gruppe == null)
            {
                return NotFound();
            }
            return View(gruppe);
        }

        // POST: Gruppen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Gruppe gruppe)
        {
            if (id != gruppe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gruppe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GruppeExists(gruppe.Id))
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
            return View(gruppe);
        }

        // GET: Gruppen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gruppe = await _context.Gruppen
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gruppe == null)
            {
                return NotFound();
            }

            return View(gruppe);
        }

        // POST: Gruppen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gruppe = await _context.Gruppen.FindAsync(id);
            if (gruppe != null)
            {
                _context.Gruppen.Remove(gruppe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GruppeExists(int id)
        {
            return _context.Gruppen.Any(e => e.Id == id);
        }
    }
}
