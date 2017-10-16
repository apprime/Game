﻿using System.Collections.Generic;
using Data.Models.Nodes;
using Data.DataProviders.Locations.Interfaces;

namespace Data.DataProviders.Locations
{
    public class MockedRegionData : IPositionDataProvider<Region>, IKnowChildren<Sector>, IKnowParent<Continent>
    {
        public Region Get(string regionId)
        {
            throw new System.NotImplementedException();
        }


        public IEnumerable<Sector> GetChildren(string id)
        {
            throw new System.NotImplementedException();
        }

        public Continent GetParent(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
