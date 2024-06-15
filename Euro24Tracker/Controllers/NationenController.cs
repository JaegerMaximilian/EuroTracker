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
    public class NationenController : Controller
    {
        private readonly Euro24TrackerContext _context;

        public NationenController(Euro24TrackerContext context)
        {
            _context = context;
        }

        // GET: Nationen
        public async Task<IActionResult> Index()
        {
            var euro24TrackerContext = _context.Nationen.Include(n => n.Gruppe);
            return View(await euro24TrackerContext.ToListAsync());
        }

        // GET: Nationen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nation = await _context.Nationen
                .Include(n => n.Gruppe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nation == null)
            {
                return NotFound();
            }

            return View(nation);
        }

        // GET: Nationen/Create
        public IActionResult Create()
        {
            ViewBag.Nationen = _context.Gruppen.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();

            //ViewData["GruppeId"] = new SelectList(_context.Gruppen, "Id", "Id");
            return View();
        }

        // POST: Nationen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ShortName,Name,LogoLink,Punkte,Tore,Gegentore,Torverhältnis,GruppeId")] Nation nation, int SelectedGruppeId)
        {
            if (ModelState.IsValid)
            {
                nation.GruppeId = SelectedGruppeId;
                nation.Gruppe = _context.Gruppen.FirstOrDefault(a => a.Id == SelectedGruppeId);
                _context.Add(nation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GruppeId"] = new SelectList(_context.Gruppen, "Id", "Id", nation.GruppeId);
            return View(nation);
        }

        // GET: Nationen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nation = await _context.Nationen.FindAsync(id);
            if (nation == null)
            {
                return NotFound();
            }

            ViewBag.GruppenNationen = _context.Nationen.Include(e=>e.Gruppe).Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.GruppeId.ToString(),
            }).ToList();

            ViewBag.Gruppen = _context.Gruppen.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();

            //ViewData["GruppeId"] = new SelectList(_context.Gruppen, "Id", "Id", nation.GruppeId);
            return View(nation);
        }

        // POST: Nationen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ShortName,Name,LogoLink,Punkte,Tore,Gegentore,Torverhältnis,GruppeId")] Nation nation, int SelectedGruppeId)
        {
            if (id != nation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    nation.GruppeId = SelectedGruppeId;
                    nation.Gruppe = _context.Gruppen.FirstOrDefault(a => a.Id == SelectedGruppeId);
                    _context.Update(nation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NationExists(nation.Id))
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
            ViewData["GruppeId"] = new SelectList(_context.Gruppen, "Id", "Id", nation.GruppeId);
            return View(nation);
        }

        // GET: Nationen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nation = await _context.Nationen
                .Include(n => n.Gruppe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nation == null)
            {
                return NotFound();
            }

            return View(nation);
        }

        // POST: Nationen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nation = await _context.Nationen.FindAsync(id);
            if (nation != null)
            {
                _context.Nationen.Remove(nation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NationExists(int id)
        {
            return _context.Nationen.Any(e => e.Id == id);
        }
    }
}
