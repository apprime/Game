using System.Collections.Generic;

namespace Data.Models.Nodes
{
    public class Continent
    {
        public IEnumerable<Region> Regions { get; }
        public World World { get; }
    }
}
