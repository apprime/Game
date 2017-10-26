using Data.DataProviders.Locations;
using Data.Models.Nodes;
using System.Collections.Generic;

namespace Data.Repositories.Nodes
{
    public class ContinentRepository
    {
        private IPositionDataProvider<Continent> _dataProvider;
        private Dictionary<Position, Continent> _data = new Dictionary<Position, Continent>();

        public ContinentRepository(IPositionDataProvider<Continent> dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public Continent Get(Position position)
        {
            return TodoCache(position);
        }

        private Continent TodoCache(Position position)
        {
            if (_data.TryGetValue(position, out Continent value))
            {
                //updateCache
                return value;
            }
            else
            {
                //TODO: Continent should maybe use a string of 1 char just to conform? Otherwise generic interface wont work
                var newValue = _dataProvider.Get(position);
                //setCache
                return newValue;
            }
        }
    }
}