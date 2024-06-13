using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Euro24Tracker.Types;
using Euro24Tracker.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using Euro24Tracker.Data;



namespace CBC_IL2.Controllers
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
            return await _context.Gruppen.Include(e => e.Nationen).ToListAsync();
        }


      
        #endregion

    }


}

