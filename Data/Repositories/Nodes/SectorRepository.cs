using Data.Models.Nodes;
using System.Collections.Generic;

namespace Data.Repositories.Nodes
{
    public class SectorRepository
    {
        private static Dictionary<Position, Sector> _data = new Dictionary<Position, Sector>();
        private static byte id = 1;
        private static string name = "SectorLand";
        private static Region parent = new Region(1, "Regionistan", continent);
        private static Position dummy = Position.FromNumbers(1, 1, 1, 0);
        private static Continent continent = new Continent(1, "InContinent");

        static SectorRepository()
        {
            _data.Add(dummy, new Sector(id, name, parent));
        }

        public SectorRepository()
        {
            
        }

        internal Sector Get(Position position)
        {
            return _data[position.StripLocation()];
        }
    }
}
