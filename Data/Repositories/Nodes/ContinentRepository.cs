using Data.Models.Nodes;
using System.Collections.Generic;

namespace Data.Repositories.Nodes
{
    internal class ContinentRepository
    {
        private Dictionary<char, Continent> _data = new Dictionary<char, Continent>();

        public ContinentRepository()
        {
        }

        internal Continent Get(char continentId)
        {
            return _data[continentId];
        }
    }
}