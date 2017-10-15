using Data.Models.Entities.EntityInterfaces;
using Data.Models.Gamestate;
using Data.Models.Nodes;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories.Nodes
{
    public class SceneRepository
    {
        public Scene Get(Location location, IEnumerable<IEntity> party)
        {
            if (location is GlobalLocation)
            {
                //Global Locations must always have only one scene
                return location.Scenes.First();
            }

            //Rule: A party can never create more than one Scene per location. FirstOrDefault is therefor all we need.
            return location.Scenes.FirstOrDefault(i => IsPartyInScene(party, i));
        }

        public Scene Create(Location location)
        {
            return new Scene(location);
        }

        private static bool IsPartyInScene(IEnumerable<IEntity> party, Scene scene)
        {
            return scene.Players.Intersect(party).Any();
        }
    }
}
