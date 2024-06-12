using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EURO2024App.Model
{
    public class Ereignis
    {
        public int Id { get; set; }
        public int? Minute { get; set; }

        public string Kommentar { get; set; }
        //public bool TorN1 {  get; set; }
        //public bool TorN2 { get; set; }

        // Navigation Properties
        public int SpielId { get; set; }
        public Spiel Spiel { get; set; }

        public int? EreignisTypId { get; set; }
        public EreignisTyp? EreignisTyp { get; set; }

        public int? TorNationId { get; set; }
        public Nation? TorNation { get; set; }
    }
}
