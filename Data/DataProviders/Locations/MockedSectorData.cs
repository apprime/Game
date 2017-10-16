using System.Collections.Generic;
using Data.Models.Nodes;
using Data.DataProviders.Locations.Interfaces;

namespace Data.DataProviders.Locations
{
    public class MockedSectorData : IPositionDataProvider<Sector>, IKnowChildren<Location>, IKnowParent<Region>
    {
        public Sector Get(string id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Location> GetChildren(string id)
        {
            throw new System.NotImplementedException();
        }

        public Region GetParent(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
