using System.ComponentModel.DataAnnotations;


namespace EURO2024App.Types
{
    public class EreignisTyp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageLink { get; set; }

        public ICollection<Ereignis>? Ereignisse { get; set; }
    }
}
