using Data.Models.Nodes;
using System.Collections.Generic;

namespace Data.Repositories.Nodes
{
    public class SectorRepository
    {
        private Dictionary<string, Sector> _data = new Dictionary<string, Sector>();

        public SectorRepository()
        {

        }

        internal Sector Get(string sectorId)
        {
            return _data[sectorId];
        }
    }
}
