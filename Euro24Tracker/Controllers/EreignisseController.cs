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
    public class EreignisseController : Controller
    {
        private readonly Euro24TrackerContext _context;

        public EreignisseController(Euro24TrackerContext context)
        {
            _context = context;
        }

        // GET: Ereignisse
        public async Task<IActionResult> Index()
        {
            var euro24TrackerContext = _context.Ereignisse.Include(e => e.EreignisTyp).Include(e => e.Spiel).ThenInclude(e=>e.Nationen).Include(e => e.TorNation).Include(e => e.Torschuetze);
            return View(await euro24TrackerContext.ToListAsync());
        }

        // GET: Ereignisse/Details/5
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
                .Include(e => e.Torschuetze)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ereignis == null)
            {
                return NotFound();
            }

            return View(ereignis);
        }

        // GET: Ereignisse/Create
        public IActionResult Create()
        {
            ViewData["EreignisTypId"] = new SelectList(_context.EreignisTyp, "Id", "Name");
            ViewData["SpielId"] = _context.Spiele.Include(e=>e.Nationen).Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.ToString()
            }).ToList();
            ViewData["TorNationId"] = new SelectList(_context.Nationen, "Id", "Name");
            ViewData["TorschuetzeId"] = new SelectList(_context.Spieler, "Id", "Name");
            return View();
        }

        // POST: Ereignisse/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Minute,Kommentar,SpielId,EreignisTypId,TorNationId,TorschuetzeId")] Ereignis ereignis)
        {
            if (ModelState.IsValid)
            {
                var ereignisTyp = _context.EreignisTyp.FirstOrDefault(e => e.Id == ereignis.EreignisTypId);
                var spiel = _context.Spiele.FirstOrDefault(e => e.Id == ereignis.SpielId);
                var torNation = _context.Nationen.FirstOrDefault(e => e.Id == ereignis.TorNationId);
                var torschuetze = _context.Spieler.FirstOrDefault(e => e.Id == ereignis.TorschuetzeId);
                ereignis.EreignisTyp = ereignisTyp;
                ereignis.Spiel = spiel;
                ereignis.TorNation = torNation;
                ereignis.Torschuetze = torschuetze;
                _context.Add(ereignis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EreignisTypId"] = new SelectList(_context.EreignisTyp, "Id", "Name", ereignis.EreignisTypId);
            ViewData["SpielId"] = _context.Spiele.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Nationen.ToString()
            }).ToList(); 
            ViewData["TorNationId"] = new SelectList(_context.Nationen, "Id", "Name", ereignis.TorNationId);
            ViewData["TorschuetzeId"] = new SelectList(_context.Spieler, "Id", "Name", ereignis.TorschuetzeId);
            return View(ereignis);
        }

        // GET: Ereignisse/Edit/5
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
            ViewData["EreignisTypId"] = new SelectList(_context.EreignisTyp, "Id", "Name", ereignis.EreignisTypId);
            ViewData["SpielId"] = _context.Spiele.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Nationen.ToString()
            }).ToList(); 
            ViewData["TorNationId"] = new SelectList(_context.Nationen, "Id", "Name", ereignis.TorNationId);
            ViewData["TorschuetzeId"] = new SelectList(_context.Spieler, "Id", "Name", ereignis.TorschuetzeId);
            return View(ereignis);
        }

        // POST: Ereignisse/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Minute,Kommentar,SpielId,EreignisTypId,TorNationId,TorschuetzeId")] Ereignis ereignis)
        {
            if (id != ereignis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var ereignisTyp = _context.EreignisTyp.FirstOrDefault(e => e.Id == ereignis.EreignisTypId);
                    var spiel = _context.Spiele.FirstOrDefault(e => e.Id == ereignis.SpielId);
                    var torNation = _context.Nationen.FirstOrDefault(e => e.Id == ereignis.TorNationId);
                    var torschuetze = _context.Spieler.FirstOrDefault(e => e.Id == ereignis.TorschuetzeId);
                    ereignis.EreignisTyp = ereignisTyp;
                    ereignis.Spiel = spiel;
                    ereignis.TorNation = torNation;
                    ereignis.Torschuetze = torschuetze;
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
            //_context.Nationen.ToList()[0].Name + _context.Nationen.ToList()[1].Name
            ViewData["EreignisTypId"] = new SelectList(_context.EreignisTyp, "Id", "Name", ereignis.EreignisTypId);
            ViewBag.Nationen =
            //ViewData["SpielId"] = new SelectList(_context.Spiele, "Id", "Id", ereignis.SpielId);
            ViewData["SpielId"] = _context.Spiele.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Nationen.ToString()
            }).ToList();
            ViewData["TorNationId"] = new SelectList(_context.Nationen, "Id", "Name", ereignis.TorNationId);
            ViewData["TorschuetzeId"] = new SelectList(_context.Spieler, "Id", "Name", ereignis.TorschuetzeId);
            return View(ereignis);
        }

        // GET: Ereignisse/Delete/5
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
                .Include(e => e.Torschuetze)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ereignis == null)
            {
                return NotFound();
            }

            return View(ereignis);
        }

        // POST: Ereignisse/Delete/5
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
