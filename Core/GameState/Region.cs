using Core.Entities;
using System.Collections.Generic;

namespace Core.Gamestate
{
    public class Region
    {
        public IEnumerable<Location> Locations { get; set; }
    }
}