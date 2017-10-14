using Data.Models.Nodes;
using System.Collections.Generic;

namespace Data.Repositories
{
    /// <summary>
    /// Todo: Resource Locator should keep references to Repositories.
    /// Repository should implement one of available data sources.
    /// Repository should have cache, but should it be singleton?
    /// Should Generators call Repositories when creating Scenes or loading content?
    /// </summary>
    public class RegionRepository
    {
        private Dictionary<char, Region> _data = new Dictionary<char, Region>();

        public RegionRepository()
        {
            _data.Add('1', new Region());
        }

        public Region Get(char identifier)
        {
            return _data[identifier];
        }
    }
}
