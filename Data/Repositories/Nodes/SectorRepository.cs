using Data.Models.Nodes;
using System.Collections.Generic;

namespace Data.Repositories.Nodes
{
    public class SectorRepository
    {
        private static Dictionary<Position, Sector> _data = new Dictionary<Position, Sector>();
        private static Position dummy = Position.FromString("001001001001");

        static SectorRepository()
        {
            _data.Add(dummy, new Sector());
        }

        public SectorRepository()
        {
            
        }

        internal Sector Get(Position position)
        {
            return _data[position];
        }
    }
}
