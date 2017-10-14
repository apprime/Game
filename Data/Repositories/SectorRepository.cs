using Data.Models.Nodes;
using System.Collections.Generic;
using System;

namespace Data.Repositories
{
    public class SectorRepository
    {
        private Dictionary<char, Sector> _data = new Dictionary<char, Sector>();

        public SectorRepository()
        {

        }

        internal Sector Get(char sectorId)
        {
            return _data[sectorId];
        }
    }
}
