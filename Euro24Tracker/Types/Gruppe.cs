using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;


namespace Euro24Tracker.Types
{
    public class Gruppe
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        // Navigation Properties
        public ICollection<Nation>? Nationen { get; set; } = new List<Nation>();

        public Gruppe()
        {
            
            Name = "";
        }

    }

    
}
