using System.Collections.Generic;

namespace Data.Models.Nodes
{
    public class Continent : IParentLocation
    {
        public byte Id { get; set; }
        public IEnumerable<Region> Regions { get; set; }
        public World World { get; set;  }
        public string Name { get; }

        public Continent(byte id, string name)
        {
            Id = id;
            Name = name;
        }

        public bool IsParent(Position p)
        {
            return p.Continent == Id;
        }
    }
}
