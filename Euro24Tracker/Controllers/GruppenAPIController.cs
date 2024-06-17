using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Euro24Tracker.Types;
using Euro24Tracker.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using Euro24Tracker.Data;



namespace Euro24Tracker.Controllers
{
    [Route("api/gruppen")] // ist immer hardgecoded, auch in der Industrie, darf sich halt nie ändern weil sonst müssen wir es hier auch ändern
    [ApiController]
    public class GruppenAPIController : ControllerBase
    {

        #region private Members

        private readonly Euro24TrackerContext _context;

        #endregion

        #region Constructor

        public GruppenAPIController(Euro24TrackerContext context)
        {
            _context = context;
        }

        #endregion

        #region methods

        [HttpGet]
        [Route("List")]
        public async Task<List<Gruppe>> ListGruppe()
        {
            foreach (Spieler spieler in _context.Spieler.Include(e => e.TorEreignisse))
            {
                spieler.Tore = spieler.TorEreignisse.Count();
            }


            foreach (Nation nation in _context.Nationen
                    .Include(e => e.TorEreginisse)
                    .Include(e => e.Spiele)
                        .ThenInclude(e=>e.Nationen)
                    .Include(e => e.Spiele)
                        .ThenInclude(e => e.Ereignisse)
                    .Include(e => e.Spieler))
            {

                nation.Tore = _context.Ereignisse
                   .Include(e => e.Spiel)
                       .ThenInclude(e => e.Nationen)
                   .Where(e => e.Spiel.Gruppenphase == true && e.Spiel.Nationen.Contains(nation) && e.TorNationId != null && e.TorNationId == nation.Id).Count();
                nation.Gegentore = _context.Ereignisse
                    .Include(e => e.Spiel)
                        .ThenInclude(e => e.Nationen)
                    .Where(e => e.Spiel.Gruppenphase == true && e.Spiel.Nationen.Contains(nation) && e.TorNationId != null && e.TorNationId != nation.Id).Count();
                nation.Torverhältnis = nation.Tore - nation.Gegentore;

                nation.Punkte = 0;
                foreach (Spiel spiel in nation.Spiele)
                {
                    if(spiel.Ereignisse.Count() > 0)
                    {
                        int tore = spiel.Ereignisse.Where(e => e.Spiel.Gruppenphase == true && e.TorNationId != null && e.TorNationId == nation.Id).Count();
                        int gegentore = spiel.Ereignisse.Where(e => e.Spiel.Gruppenphase == true && e.TorNationId != null && e.TorNationId != nation.Id).Count();
                        if (tore > gegentore)
                        {
                            nation.Punkte += 3;
                        }
                        else if (tore == gegentore)
                        {
                            nation.Punkte += 1;
                        }
                    }
                   
                }
            }
            await _context.SaveChangesAsync();

            return await _context.Gruppen
                .Include(e => e.Nationen)
                    .ThenInclude(e=>e.Spiele)
                .Include(e => e.Nationen)
                    .ThenInclude(e => e.Spieler)
                 .ToListAsync();
        }


        //[HttpPost]
        //[Route("Create")]
        //public async Task<IActionResult> CreateNewMovie([FromBody] Gruppe gruppe)
        //{
        //    // specify

        //    // check in db
        //    _context.Gruppen.Add(gruppe);
        //    await _context.SaveChangesAsync();


        //    // return sth

        //    return base.Created(); // base ist nicht nötig aber gut zum suchen mit intellisense
        //}


        #endregion

    }


}

