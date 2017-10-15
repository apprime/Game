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
        private Dictionary<string, Region> _data = new Dictionary<string, Region>();

        public RegionRepository()
        {
            _data.Add("11", new Region());
        }

        public Region Get(string regionId)
        {
            return _data[regionId];
        }
    }
}
