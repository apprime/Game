using Data.Models.Nodes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.DataProviders.Locations
{
    class PersistentLocationData : IPositionDataProvider<Location>
    {
        public Task<Location> Get(Position position)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Location> GetAllForParent(byte parentId)
        {
            throw new NotImplementedException();
        }
    }
}
