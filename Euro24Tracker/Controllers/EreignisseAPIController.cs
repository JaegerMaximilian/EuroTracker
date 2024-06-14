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
            

            _context.Ereignisse.Add(ereignis);
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

