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
    public class NationsController : Controller
    {
        private readonly Euro24TrackerContext _context;

        public NationsController(Euro24TrackerContext context)
        {
            _context = context;
        }

        // GET: Nations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Nation.ToListAsync());
        }

        // GET: Nations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nation = await _context.Nation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nation == null)
            {
                return NotFound();
            }

            return View(nation);
        }

        // GET: Nations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ShortName,Name,LogoLink")] Nation nation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nation);
        }

        // GET: Nations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nation = await _context.Nation.FindAsync(id);
            if (nation == null)
            {
                return NotFound();
            }
            return View(nation);
        }

        // POST: Nations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ShortName,Name,LogoLink")] Nation nation)
        {
            if (id != nation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            return View(nation);
        }

        // GET: Nations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nation = await _context.Nation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nation == null)
            {
                return NotFound();
            }

            return View(nation);
        }

        // POST: Nations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nation = await _context.Nation.FindAsync(id);
            if (nation != null)
            {
                _context.Nation.Remove(nation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NationExists(int id)
        {
            return _context.Nation.Any(e => e.Id == id);
        }
    }
}
