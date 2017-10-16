using Data.DataProviders.Locations;
using Data.Models.Nodes;
using System.Collections.Generic;

namespace Data.Repositories.Nodes
{
    public class ContinentRepository
    {
        private IPositionDataProvider<Continent> _dataProvider;
        private Dictionary<char, Continent> _data = new Dictionary<char, Continent>();

        public ContinentRepository(IPositionDataProvider<Continent> dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public Continent Get(char continentId)
        {
            return TodoCache(continentId);
        }

        private Continent TodoCache(char continentId)
        {
            if (_data.TryGetValue(continentId, out Continent value))
            {
                //updateCache
                return value;
            }
            else
            {
                //TODO: Continent should maybe use a string of 1 char just to conform? Otherwise generic interface wont work
                var newValue = _dataProvider.Get(continentId.ToString());
                //setCache
                return newValue;
            }
        }
    }
}