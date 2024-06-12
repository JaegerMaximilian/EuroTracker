using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EURO2024App.Model
{
    public class SpielNation
    {
        public int SpielId { get; set; }
        public Spiel Spiel { get; set; }

        public int NationId { get; set; }
        public Nation Nation { get; set; }

        public int? Tore { get; set; }
    }
}
