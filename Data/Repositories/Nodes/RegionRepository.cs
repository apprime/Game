using Data.Models.Nodes;
using System.Collections.Generic;

namespace Data.Repositories.Nodes
{
    /// <summary>
    /// Todo: Resource Locator should keep references to Repositories.
    /// Repository should implement one of available data sources.
    /// Repository should have cache, but should it be singleton?
    /// Should Generators call Repositories when creating Scenes or loading content?
    /// </summary>
    public class RegionRepository
    {
        private Dictionary<Position, Region> _data = new Dictionary<Position, Region>();

        public RegionRepository()
        {
            _data.Add(Position.FromNumbers(1,1,1,0), 
                      new Region(1, "Regionistan", new Continent(1, "InContinent")));
        }

        public Region Get(Position position)
        {
            return _data[position.StripSector()];
        }
    }
}
