using Data.Models.Entities;
using System.Collections.Generic;

namespace Data.Models.Gamestate
{
    public class Region
    {
        public IEnumerable<Location> Locations { get; set; }
    }
}