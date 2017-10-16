using System.Collections.Generic;
using Data.Models.Exceptions;
using Data.Models.Nodes;
using Data.DataProviders.Locations.Interfaces;

namespace Data.DataProviders.Locations
{
    public class MockedContinentData : IPositionDataProvider<Continent>, IKnowChildren<Region>
    {
        public Continent Get(char continentId)
        {
            //TODO: Fix this so that we have non hardcoded stuff.
            switch(continentId)
            {
                case '1':
                    return SetupContinent(continentId);
                default:
                    throw new TodoException("Include more continents!");
            }
        }

        public Continent Get(string id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Region> GetChildren(string id)
        {
            throw new System.NotImplementedException();
        }

        private Continent SetupContinent(char continentId)
        {
            var continent = new Continent();
            //continent.Regions = new MockedRegionData().GetChildren(continentId.ToString());
            return continent;

        }
    }
}
