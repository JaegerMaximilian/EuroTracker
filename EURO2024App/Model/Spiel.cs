using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EURO2024App.Model
{
    public class Spiel
    {
        public int Id { get; set; }
        public string Stadion { get; set; }
        public bool Gruppenphase { get; set; }
        //public Nation Nation1 { get; set; }
        //public Nation Nation2 { get; set;}
        //public List<Ereignis>? Ereignisse { get; set; } = [];
        //public int ToreN1 { get; set; }
        //public int ToreN2 { get;set; }

        // Navigation Properties
        public ICollection<SpielNation>? SpielNationen { get; set; }
        public ICollection<Ereignis>? Ereignisse { get; set; }
    }

    [JsonSerializable(typeof(List<Spiel>))]
    internal sealed partial class SpielContext : JsonSerializerContext
    {

    }
}
