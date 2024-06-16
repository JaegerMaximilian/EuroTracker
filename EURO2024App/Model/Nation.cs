using Euro24Tracker.Types;
using System.ComponentModel.DataAnnotations;


namespace EURO2024App.Model
{
    public class Nation
    {
        public int Id { get; set; }
        
        public string ShortName { get; set; }

        public string? Name { get; set; }
        public string LogoLink { get; set; }
        public int? Punkte { get; set; }
        public int? Tore {  get; set; }
        public int? Gegentore { get; set; }
        public int? Torverhältnis { get; set; }


        // Navigation Properties
        public int GruppeId { get; set; }
        public Gruppe Gruppe { get; set; }

        public ICollection<Spieler>? Spieler { get; set; } = new List<Spieler>();
        public ICollection<Ereignis>? TorEreginisse { get; set; }
        public ICollection<Spiel>? Spiele { get; set; }

        public int? ToreImSpiel { get; set; }





    }

    
}
