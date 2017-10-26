using Data.Models.Entities.Humans;
using Data.Models.Nodes;

namespace Data.Models.Entities
{
    public class Movement
    {
        //TODO: We might want Traveler to be IEntity so that we can move NPCs and such also
        public Player Traveler { get; set; }
        public Location Destination { get; set; }
        public Location Origin { get; set; }
    }
}
