using System.ComponentModel.DataAnnotations;


namespace Euro24Tracker.Types
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
        public int? GruppeId { get; set; }
        public Gruppe? Gruppe { get; set; } = new Gruppe();

        public ICollection<Ereignis>? TorEreginisse { get; set; } = new List<Ereignis>();
        public ICollection<Spiel>? Spiele { get; set; } = new List<Spiel>();

        public Nation()
        {
            Id = 0;
            ShortName = "";
            LogoLink = "";
        }



    }

    
}
