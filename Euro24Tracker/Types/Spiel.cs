using System.ComponentModel.DataAnnotations;


namespace Euro24Tracker.Types
{
    public class Spiel
    {
        public int Id { get; set; }
        public string Stadion { get; set; }
        public bool Gruppenphase { get; set; }


        // Navigation Properties
        public ICollection<Nation> Nationen { get; set; } = new List<Nation>();
        public ICollection<Ereignis>? Ereignisse { get; set; } = new List<Ereignis>();

        //public Nation Nation1 { get; set; }
        //public Nation Nation2 { get; set;}
        //public List<Ereignis>? Ereignisse { get; set; } = [];
        //public int ToreN1 { get; set; }
        //public int ToreN2 { get;set; }

    }


}
