using System.ComponentModel.DataAnnotations;


namespace EURO2024App.Model
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

       public string? GruppeString { get; set; }




}


}
