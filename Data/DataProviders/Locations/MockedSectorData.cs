using System.Collections.Generic;
using Data.Models.Nodes;
using Data.DataProviders.Locations.Interfaces;

namespace Data.DataProviders.Locations
{
    public class MockedSectorData : IPositionDataProvider<Sector>, IKnowChildren<Location>, IKnowParent<Region>
    {
        public Sector Get(byte id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Location> GetChildren(byte id)
        {
            throw new System.NotImplementedException();
        }

        public Region GetParent(byte id)
        {
            throw new System.NotImplementedException();
        }
    }
}
