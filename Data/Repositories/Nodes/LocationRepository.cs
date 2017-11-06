using Data.DataProviders.Locations;
using Data.Models.Entities;
using Data.Models.Entities.EntityInterfaces;
using Data.Models.Nodes;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories.Nodes
{
    public class LocationRepository
    {
        private IPositionDataProvider<Location> _dataProvider;
        private static Dictionary<Position, List<Location>> _data = new Dictionary<Position, List<Location>>();

        public LocationRepository(IPositionDataProvider<Location> dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public LocationRepository()
        {
            _dataProvider = new MockedLocationData();
        }

        public IEnumerable<Location> Get(Position position)
        {
            return TodoCache(position);
        }

        public Location Get(Id id)
        {
            var all = Get(id.Position);
            return all.SingleOrDefault(i => i.Id == id);
        }

        public void Delete(Location location)
        {
            _data[location.Position].Remove(location);
        }

        public Location Create(Position position)
        {
            if (!_data.ContainsKey(position))
            {
                _data.Add(position, new List<Location>());
            }

            var newLocation = _dataProvider.Get(position);
            _data[position].Add(newLocation);
            return newLocation;
        }

        private IEnumerable<Location> TodoCache(Position position)
        {
            if (!_data.ContainsKey(position))
            {
                _data.Add(position, new List<Location>());
                _data[position].Add(_dataProvider.Get(position));
            }

            return _data[position];
        }
    }
}