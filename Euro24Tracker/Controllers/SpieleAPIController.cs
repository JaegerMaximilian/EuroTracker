using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Euro24Tracker.Types;
using Euro24Tracker.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using Euro24Tracker.Data;

using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text;



namespace Euro24Tracker.Controllers
{
    [Route("api/spiele")] // ist immer hardgecoded, auch in der Industrie, darf sich halt nie ändern weil sonst müssen wir es hier auch ändern
    [ApiController]
    public class SpieleAPIController : ControllerBase
    {

        #region private Members

        private readonly Euro24TrackerContext _context;

        #endregion

        #region Constructor

        public SpieleAPIController(Euro24TrackerContext context)
        {
            _context = context;
        }

        #endregion

        #region methods

        [HttpGet]
        [Route("List")]
        public async Task<List<Spiel>> ListSpiele()
        {
            

            var spiele =  await _context.Spiele
                .Include(e => e.Nationen)
                //.ThenInclude(e => e.Gruppe)
                .Include(e => e.Ereignisse).ToListAsync();

          
            return spiele;
        }


       
        #endregion

    }


}

