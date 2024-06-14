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
    public class SpieleController : Controller
    {
        private readonly Euro24TrackerContext _context;

        public SpieleController(Euro24TrackerContext context)
        {
            _context = context;
        }

        // GET: Spiele
        public async Task<IActionResult> Index()
        {
            return View(await _context.Spiele.Include(e => e.Nationen).ToListAsync());
        }

        // GET: Spiele/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spiel = await _context.Spiele.Include(e=>e.Nationen)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spiel == null)
            {
                return NotFound();
            }

            return View(spiel);
        }

        // GET: Spiele/Create
        public IActionResult Create()
        {
            ViewBag.Nationen = _context.Nationen.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.ShortName
            }).ToList();

            return View();
        }

        // POST: Spiele/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Stadion,Gruppenphase, Datetime, Nationen, Ereignisse")] Spiel spiel, int[] SelectedNationendIds)
        {
           spiel.Nationen = (spiel.Nationen  == null) ? new List<Nation>() : spiel.Nationen;

            if (ModelState.IsValid)
            {
                if (SelectedNationendIds.Length != 2)
                {
                    ViewBag.Nationen = _context.Nationen.Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.ShortName
                    }).ToList();
                    return View(spiel);
                }
                if (SelectedNationendIds != null && SelectedNationendIds.Length > 0)
                {
                    foreach (int nationId in SelectedNationendIds)
                    {
                        var nation = await _context.Nationen.FindAsync(nationId);
                        spiel.Nationen.Add(nation);
                    }
                }

                _context.Add(spiel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(spiel);
        }

        // GET: Spiele/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spiel = await _context.Spiele.FindAsync(id);

            ViewBag.Nationen = _context.Nationen.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(), 
                Text = a.ShortName 
            }).ToList();

            ViewBag.SpielNation = _context.SpielNation.Select(a => new SelectListItem
            {
                Value = a.SpielId.ToString(), 
                Text = a.NationId.ToString(), 
            }).ToList();

            if (spiel == null)
            {
                return NotFound();
            }
            return View(spiel);
        }

        // POST: Spiele/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Stadion,Gruppenphase,Nationen,Ereignisse")] Spiel spiel, int[] SelectedNationendIds)
        {
            if (id != spiel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (SelectedNationendIds != null && SelectedNationendIds.Length > 0)
                {
                    if (SelectedNationendIds.Length != 2)
                    {
                        ViewBag.Nationen = _context.Nationen.Select(a => new SelectListItem
                        {
                            Value = a.Id.ToString(),
                            Text = a.ShortName
                        }).ToList();

                        ViewBag.SpielNation = _context.SpielNation.Select(a => new SelectListItem
                        {
                            Value = a.SpielId.ToString(),
                            Text = a.NationId.ToString(),
                        }).ToList();
                        return View(spiel);
                    }
                    foreach (var spielnation in await _context.SpielNation.ToListAsync())
                    {
                        if (spielnation.SpielId == id)
                        {
                            _context.SpielNation.Remove(spielnation);
                        }
                    }

                    _context.Update(spiel);
                    await _context.SaveChangesAsync();

                    foreach (int nationId in SelectedNationendIds)
                    {
                        var nation = await _context.Nationen.FindAsync(nationId);
                        spiel.Nationen.Add(nation);
                    }
                }

                try
                {
                    _context.Update(spiel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpielExists(spiel.Id))
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
            return View(spiel);
        }

        // GET: Spiele/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spiel = await _context.Spiele
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spiel == null)
            {
                return NotFound();
            }

            return View(spiel);
        }

        // POST: Spiele/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spiel = await _context.Spiele.FindAsync(id);
            if (spiel != null)
            {
                _context.Spiele.Remove(spiel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpielExists(int id)
        {
            return _context.Spiele.Any(e => e.Id == id);
        }
    }
}
