using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EURO2024App.Model
{
    public class Gruppe
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // Navigation Properties
        public ICollection<Nation>? Nationen { get; set; }
    }
}
