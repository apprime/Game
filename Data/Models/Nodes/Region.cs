using System.Collections.Generic;

namespace Data.Models.Nodes
{
    public class Region : IParentLocation
    {
        public byte Id { get; set; }
        public string Name { get; }
        public Continent Continent { get; }
        public IEnumerable<Sector> Sectors{ get; }

        public Region(byte id, string name, Continent parent)
        {
            Id = id;
            Name = name;
            Continent = parent;
        }

        public bool IsParent(Position p)
        {
            return Continent.IsParent(p) && p.Region == Id;
        }
    }
}
