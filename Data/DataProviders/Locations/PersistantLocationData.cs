using Data.Models.Nodes;
using System;
using System.Collections.Generic;

namespace Data.DataProviders.Locations
{
    class PersistantLocationData : IPositionDataProvider<Location>
    {
        public Location Get(byte locationId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Location> GetAllForParent(byte parentId)
        {
            throw new NotImplementedException();
        }
    }
}
