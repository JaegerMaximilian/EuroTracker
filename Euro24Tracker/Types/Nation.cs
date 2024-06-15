using System.ComponentModel.DataAnnotations;


namespace Euro24Tracker.Types
{
    public class Nation
    {
        public int Id { get; set; }
        
        public string ShortName { get; set; }

        public string? Name { get; set; }
        public string LogoLink { get; set; }
        public int? Punkte { get; set; } = 0;
        public int? Tore { get; set; } = 0;
        public int? Gegentore { get; set; } = 0;
        public int? Torverhältnis { get; set; } = 0;


        // Navigation Properties
        public int? GruppeId { get; set; }
        public Gruppe? Gruppe { get; set; } = new Gruppe();

        public ICollection<Ereignis>? TorEreginisse { get; set; } = new List<Ereignis>();
        public ICollection<Spiel>? Spiele { get; set; } = new List<Spiel>();

        public Nation()
        {
            
            ShortName = "";
            LogoLink = "";
            Punkte = 0;
            Tore = 0;
            Gegentore = 0;
            Torverhältnis = 0;
        }



    }

    
}
