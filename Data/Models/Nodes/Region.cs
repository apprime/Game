using System.Collections.Generic;

namespace Data.Models.Nodes
{
    public class Region
    {
        public Continent Continent { get; }
        public IEnumerable<Location> Locations { get; }
    }
}
