using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;


namespace Euro24Tracker.Types
{
    public class EreignisTyp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageLink { get; set; }

        public ICollection<Ereignis>? Ereignisse { get; set; } = new List<Ereignis>();

        public EreignisTyp()
        {
            
            Name = "";
            ImageLink = "";
        }
    }
}
