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
    [Route("api/ereignistypen")] // ist immer hardgecoded, auch in der Industrie, darf sich halt nie ändern weil sonst müssen wir es hier auch ändern
    [ApiController]
    public class EreignisTypenAPIController : ControllerBase
    {

        #region private Members

        private readonly Euro24TrackerContext _context;

        #endregion

        #region Constructor

        public EreignisTypenAPIController(Euro24TrackerContext context)
        {
            _context = context;
        }

        #endregion

        #region methods

        [HttpGet]
        [Route("List")]
        public async Task<List<EreignisTyp>> ListEreignisse()
        {
            return await _context.EreignisTyp.ToListAsync();
        }

       


        #endregion

    }


}

