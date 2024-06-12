using System.ComponentModel.DataAnnotations;


namespace Euro24Tracker.Types
{
    public class Gruppe
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        // Navigation Properties
        public ICollection<Nation>? Nationen { get; set; }



    }

    
}
