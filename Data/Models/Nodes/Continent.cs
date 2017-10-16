using System.Collections.Generic;

namespace Data.Models.Nodes
{
    public class Continent
    {
        public IEnumerable<Region> Regions { get; set; }
        public World World { get; set;  }
    }
}
