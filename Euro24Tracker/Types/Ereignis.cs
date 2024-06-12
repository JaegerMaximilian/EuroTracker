using System.ComponentModel.DataAnnotations;


namespace Euro24Tracker.Types
{
    public class Ereignis
    {
        public int Id { get; set; }
        public int? Minute { get; set; }

        public string Kommentar {  get; set; }
        //public bool TorN1 {  get; set; }
        //public bool TorN2 { get; set; }

        // Navigation Properties
        public int SpielId { get; set; }
        public Spiel Spiel { get; set; }

        public int? EreignisTypId { get; set; }
        public EreignisTyp? EreignisTyp {  get; set; }

        public int? TorNationId { get; set; }
        public Nation? TorNation { get; set; }
    }
}
