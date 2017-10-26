using System.Collections.Generic;
using Data.Models.Nodes;
using Data.DataProviders.Locations.Interfaces;

namespace Data.DataProviders.Locations
{
    public class MockedRegionData : IPositionDataProvider<Region>, IKnowChildren<Sector>, IKnowParent<Continent>
    {
        public Region Get(Position pos)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Sector> GetChildren(byte id)
        {
            throw new System.NotImplementedException();
        }

        public Continent GetParent(byte id)
        {
            throw new System.NotImplementedException();
        }
    }
}
