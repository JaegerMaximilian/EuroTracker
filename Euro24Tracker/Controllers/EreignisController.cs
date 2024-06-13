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
    public class EreignisController : Controller
    {
        private readonly Euro24TrackerContext _context;

        public EreignisController(Euro24TrackerContext context)
        {
            _context = context;
        }

        // GET: Ereignis
        public async Task<IActionResult> Index()
        {
            var euro24TrackerContext = _context.Ereignisse.Include(e => e.EreignisTyp).Include(e => e.Spiel).Include(e => e.TorNation);
            return View(await euro24TrackerContext.ToListAsync());
        }

        // GET: Ereignis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ereignis = await _context.Ereignisse
                .Include(e => e.EreignisTyp)
                .Include(e => e.Spiel)
                .Include(e => e.TorNation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ereignis == null)
            {
                return NotFound();
            }

            return View(ereignis);
        }

        // GET: Ereignis/Create
        public IActionResult Create()
        {
            ViewData["EreignisTypId"] = new SelectList(_context.EreignisTyp, "Id", "Id");
            ViewData["SpielId"] = new SelectList(_context.Spiele, "Id", "Id");
            ViewData["TorNationId"] = new SelectList(_context.Nationen, "Id", "Id");
            return View();
        }

        // POST: Ereignis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Minute,Kommentar,SpielId,EreignisTypId,TorNationId")] Ereignis ereignis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ereignis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EreignisTypId"] = new SelectList(_context.EreignisTyp, "Id", "Id", ereignis.EreignisTypId);
            ViewData["SpielId"] = new SelectList(_context.Spiele, "Id", "Id", ereignis.SpielId);
            ViewData["TorNationId"] = new SelectList(_context.Nationen, "Id", "Id", ereignis.TorNationId);
            return View(ereignis);
        }

        // GET: Ereignis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ereignis = await _context.Ereignisse.FindAsync(id);
            if (ereignis == null)
            {
                return NotFound();
            }
            ViewData["EreignisTypId"] = new SelectList(_context.EreignisTyp, "Id", "Id", ereignis.EreignisTypId);
            ViewData["SpielId"] = new SelectList(_context.Spiele, "Id", "Id", ereignis.SpielId);
            ViewData["TorNationId"] = new SelectList(_context.Nationen, "Id", "Id", ereignis.TorNationId);
            return View(ereignis);
        }

        // POST: Ereignis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Minute,Kommentar,SpielId,EreignisTypId,TorNationId")] Ereignis ereignis)
        {
            if (id != ereignis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ereignis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EreignisExists(ereignis.Id))
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
            ViewData["EreignisTypId"] = new SelectList(_context.EreignisTyp, "Id", "Id", ereignis.EreignisTypId);
            ViewData["SpielId"] = new SelectList(_context.Spiele, "Id", "Id", ereignis.SpielId);
            ViewData["TorNationId"] = new SelectList(_context.Nationen, "Id", "Id", ereignis.TorNationId);
            return View(ereignis);
        }

        // GET: Ereignis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ereignis = await _context.Ereignisse
                .Include(e => e.EreignisTyp)
                .Include(e => e.Spiel)
                .Include(e => e.TorNation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ereignis == null)
            {
                return NotFound();
            }

            return View(ereignis);
        }

        // POST: Ereignis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ereignis = await _context.Ereignisse.FindAsync(id);
            if (ereignis != null)
            {
                _context.Ereignisse.Remove(ereignis);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EreignisExists(int id)
        {
            return _context.Ereignisse.Any(e => e.Id == id);
        }
    }
}
