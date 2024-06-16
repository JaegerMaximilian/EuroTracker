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
            return await _context.Gruppen
                .Include(e => e.Nationen)
                    .ThenInclude(e=>e.Spiele)
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

