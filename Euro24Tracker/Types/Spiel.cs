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

        public string? Description
        {
            get
            {
                return ToString();
            }
        }

        public Spiel()
        {
            
            Stadion = "";
            Gruppenphase = true;

        }

        public override string ToString()
        {
                if (Nationen.Count >= 2)
                {
                    string result = $"{Nationen.ToList()[0].Name} : {Nationen.ToList()[1].Name}";
                    return result;
                }
            else
            {
                return "No Nations";
            }

        }
        }

   


}
