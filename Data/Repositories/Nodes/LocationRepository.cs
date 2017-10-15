using Data.DataProviders.Locations;
using Data.Models.Nodes;
using System.Collections.Generic;

namespace Data.Repositories.Nodes
{
    public class LocationRepository
    {
        private ILocationDataProvider _dataProvider;
        private Dictionary<string, Location> _data = new Dictionary<string, Location>();

        public LocationRepository(ILocationDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public  Location Get(string locationId)
        {
            return TodoCache(locationId);
        }

        private Location TodoCache(string locationId)
        {
            if(_data.TryGetValue(locationId, out Location value))
            {
                //updateCache
                return value;
            }
            else
            {
                var newValue = _dataProvider.Get(locationId);
                //setCache
                return newValue;
            }
        }
    }
}