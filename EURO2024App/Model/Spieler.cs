using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;


namespace EURO2024App.Model
{
    public class Spieler
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Tore { get; set; }


        // Navigation Properties
        public int NationId { get; set; }
        public Nation Nation { get; set; } = new Nation();

        public ICollection<Ereignis>? TorEreignisse { get; set; }

        public Spieler()
        {
            Name = "";
            Tore = 0;
        }
    }
}
