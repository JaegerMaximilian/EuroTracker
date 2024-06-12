using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EURO2024App.Model
{
    public class EreignisTyp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageLink { get; set; }

        public ICollection<Ereignis>? Ereignisse { get; set; }
    }
}
