using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Euro24Tracker.Types;
using Euro24Tracker.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using Euro24Tracker.Data;
using Euro24Tracker.Controllers;


namespace Euro24Tracker.Controllers
{
    [Route("api/ereignisse")] // ist immer hardgecoded, auch in der Industrie, darf sich halt nie ändern weil sonst müssen wir es hier auch ändern
    [ApiController]
    public class EreignisseAPIController : ControllerBase
    {

        #region private Members

        private readonly Euro24TrackerContext _context;

        #endregion

        #region Constructor

        public EreignisseAPIController(Euro24TrackerContext context)
        {
            _context = context;
        }

        #endregion

        #region methods

        [HttpGet]
        [Route("List")]
        public async Task<List<Ereignis>> ListEreignisse()
        {
            return await _context.Ereignisse.Include(e => e.Spiel).Include(e => e.TorNation).Include(e => e.EreignisTyp).ToListAsync();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateNewEreignis([FromBody] Ereignis ereignis)
        {
            if(ereignis.Spiel == null)
            {
                ereignis.Spiel = _context.Spiele
                    .Include(e=>e.Nationen)
                        .ThenInclude(e=>e.Spiele)
                    .FirstOrDefault(a => a.Id == ereignis.SpielId);
            }

          



            _context.Ereignisse.Add(ereignis);

            if (ereignis.TorNationId != null)
            {
                Nation tornation = _context.Nationen.Include(e=>e.Spiele).FirstOrDefault(a => a.Id == ereignis.TorNationId);
                tornation.Tore = (tornation.Tore == null) ? 1 : (tornation.Tore+=1);
                tornation.Torverhältnis = (tornation.Torverhältnis == null) ? 1 : (tornation.Torverhältnis+=1);

                List<Nation> exceptList = new List<Nation>();
                exceptList.Add(tornation);
                Nation gegentornation = ereignis.Spiel.Nationen.Except(exceptList).FirstOrDefault();
                Nation gegnertornation = _context.Nationen.Include(e => e.Spiele).FirstOrDefault(a => a.Id == gegentornation.Id);
                gegentornation.Gegentore = (gegentornation.Gegentore == null) ? 1 : (gegentornation.Gegentore+=1);
                gegentornation.Torverhältnis = (gegentornation.Torverhältnis == null) ? -1 : (gegentornation.Torverhältnis-=1);

                var tornationPunkte = 0;
                var gegentornationPunkte = 0;

                foreach(Spiel spiel in tornation.Spiele)
                {
                    int tore_tornation = _context.Ereignisse.Where(e => e.SpielId == spiel.Id && e.TorNationId == ereignis.TorNationId).ToList().Count() + 1;
                    int tore_gegentornation = _context.Ereignisse.Where(e => e.SpielId == spiel.Id && e.TorNationId == gegentornation.Id).ToList().Count();
                    if (tore_tornation > tore_gegentornation)
                    {
                        tornationPunkte += 3;
                    }
                    else if (tore_tornation == tore_gegentornation)
                    {
                        tornationPunkte += 1;
                    }
                }

                foreach (Spiel spiel in gegentornation.Spiele)
                {
                    int tore_tornation = _context.Ereignisse.Where(e => e.SpielId == spiel.Id && e.TorNationId == ereignis.TorNationId).ToList().Count() + 1;
                    int tore_gegentornation = _context.Ereignisse.Where(e => e.SpielId == spiel.Id && e.TorNationId == gegentornation.Id).ToList().Count();
                    if (tore_tornation < tore_gegentornation)
                    {
                        gegentornationPunkte += 3;
                    }
                    else if (tore_tornation == tore_gegentornation)
                    {
                        gegentornationPunkte += 1;
                    }
                }


                tornation.Punkte = tornationPunkte;
                gegentornation.Punkte = gegentornationPunkte;



                //int tore_tornation = _context.Ereignisse.Where(e => e.SpielId == ereignis.SpielId && e.TorNationId == ereignis.TorNationId).ToList().Count() + 1;
                //int tore_gegentornation = _context.Ereignisse.Where(e => e.SpielId == ereignis.SpielId && e.TorNationId == gegentornation.Id).ToList().Count();

                //if(tore_tornation >  tore_gegentornation)
                //{
                //    tornation.Punkte = (tornation.Punkte == null) ? 3 : tornation.Punkte += 3;
                //}
                //else if(tore_tornation < tore_gegentornation)
                //{
                //    gegentornation.Punkte = (gegentornation.Punkte == null) ? 3 : gegentornation.Punkte += 3;
                //}
                //else if(tore_tornation < tore_gegentornation)
                //{
                //    tornation.Punkte = (tornation.Punkte == null) ? 1 : tornation.Punkte += 1;
                //    gegentornation.Punkte = (gegentornation.Punkte == null) ? 1 : gegentornation.Punkte += 1;
                //}
            }

            await _context.SaveChangesAsync();

            return base.Created(); // base ist nicht nötig aber gut zum suchen mit intellisense
        }


        //[HttpDelete]
        //[Route("Delete")]
        //public async Task<IActionResult> DeleteEreignis([FromBody] int id)
        //{
            
        //    var ereignis = await _context.Ereignisse.FindAsync(id);
        //    _context.Ereignisse.Remove(ereignis);
        //    await _context.SaveChangesAsync();


        //    return base.Created(); // base ist nicht nötig aber gut zum suchen mit intellisense
        //}


        #endregion

    }


}

