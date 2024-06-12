using System.ComponentModel.DataAnnotations;


namespace Euro24Tracker.Types
{
    public class EreignisTyp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageLink { get; set; }

        public ICollection<Ereignis>? Ereignisse { get; set; }
    }
}
