using Data.Models.Entities.Humans;
using Data.Models.Gamestate;

namespace Data.Models.Entities
{
    public class Movement
    {
        //TODO: We might want Traveler to be IEntity so that we can move NPCs and such also
        public Player Traveler { get; set; }
        public Scene Destination { get; set; }
        public Scene Origin { get; set; }
    }
}
