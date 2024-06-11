using System.ComponentModel.DataAnnotations;


namespace Euro24Tracker.Types
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
