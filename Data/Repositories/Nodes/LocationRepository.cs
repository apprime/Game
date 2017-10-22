using Data.DataProviders.Locations;
using Data.Models.Nodes;
using System.Collections.Generic;

namespace Data.Repositories.Nodes
{
    public class LocationRepository
    {
        private IPositionDataProvider<Location> _dataProvider;
        private Dictionary<byte, Location> _data = new Dictionary<byte, Location>();

        public LocationRepository(IPositionDataProvider<Location> dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public LocationRepository()
        {
            _dataProvider = new MockedLocationData();
        }

        public  Location Get(Position position)
        {
            return TodoCache(position.Location);
        }

        private Location TodoCache(byte locationId)
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