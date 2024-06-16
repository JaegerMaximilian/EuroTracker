using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;


namespace Euro24Tracker.Types
{
    public class Spiel
    {
        public int Id { get; set; }
        public string Stadion { get; set; }
        public bool Gruppenphase { get; set; }

        public DateTime Datetime { get; set; }

        // Navigation Properties
        public ICollection<Nation> Nationen { get; set; } = new List<Nation>();
        public ICollection<Ereignis>? Ereignisse { get; set; } = new List<Ereignis>();

        //public string? GameName
        //{
        //    get
        //    {
        //        if (Nationen.Count >= 2)
        //        {
        //            var firstNation = Nationen.ElementAt(0).Name;
        //            var secondNation = Nationen.ElementAt(1).Name;
        //            return $"{firstNation} vs {secondNation}";
        //        }
        //        return "Nations not available";
        //    }
        //}

        public Spiel()
        {
            
            Stadion = "";
            Gruppenphase = true;

        }

    }


}
