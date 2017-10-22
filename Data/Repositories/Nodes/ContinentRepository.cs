using Data.DataProviders.Locations;
using Data.Models.Nodes;
using System.Collections.Generic;

namespace Data.Repositories.Nodes
{
    public class ContinentRepository
    {
        private IPositionDataProvider<Continent> _dataProvider;
        private Dictionary<byte, Continent> _data = new Dictionary<byte, Continent>();

        public ContinentRepository(IPositionDataProvider<Continent> dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public Continent Get(byte continentId)
        {
            return TodoCache(continentId);
        }

        private Continent TodoCache(byte continentId)
        {
            if (_data.TryGetValue(continentId, out Continent value))
            {
                //updateCache
                return value;
            }
            else
            {
                //TODO: Continent should maybe use a string of 1 char just to conform? Otherwise generic interface wont work
                var newValue = _dataProvider.Get(continentId);
                //setCache
                return newValue;
            }
        }
    }
}