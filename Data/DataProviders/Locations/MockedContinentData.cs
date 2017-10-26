using System.Collections.Generic;
using Data.Models.Exceptions;
using Data.Models.Nodes;
using Data.DataProviders.Locations.Interfaces;

namespace Data.DataProviders.Locations
{
    public class MockedContinentData : IPositionDataProvider<Continent>, IKnowChildren<Region>
    {
        public Continent Get(Position position)
        {
            //TODO: Fix this so that we have non hardcoded stuff.
            switch(position.Continent)
            {
                case 1:
                    return SetupContinent(position.Continent);
                default:
                    throw new TodoException("Include more continents!");
            }
        }

        public Continent Get(byte id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Region> GetChildren(byte id)
        {
            throw new System.NotImplementedException();
        }

        private Continent SetupContinent(byte continentId)
        {
            var continent = new Continent();
            //continent.Regions = new MockedRegionData().GetChildren(continentId.ToString());
            return continent;

        }
    }
}
