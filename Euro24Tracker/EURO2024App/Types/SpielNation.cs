using System.ComponentModel.DataAnnotations;


namespace EURO2024.Types
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
