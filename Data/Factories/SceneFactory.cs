using Data.Models.Entities.EntityInterfaces;
using Data.Models.Entities.Humans;
using Data.Models.Gamestate;
using Data.Models.Nodes;
using Data.Repositories.Nodes;
using System.Collections.Generic;
using System.Linq;

namespace Data.Factories
{
    public static class SceneFactory
    {
        public static Scene GetOrCreate(Position position, Player player)
        {
            var repo = new LocationRepository(); //TODO: Manage this in another way
            var location = repo.Get(position);

            var candidate = Get(location, player.Party);
            if (candidate == null)
            {
                return Create(location);
            }
            else
            {
                return candidate;
            }
        }

        private static Scene Get(Location location, IEnumerable<IEntity> party)
        {
            if (location is GlobalLocation)
            {
                //Global Locations must always have only one scene
                return location.Scenes.First();
            }

            //Rule: A party can never create more than one Scene per location. FirstOrDefault is therefor all we need.
            return location.Scenes.FirstOrDefault(i => IsPartyInScene(party, i));
        }

        public static Scene Create(Location location)
        {
            var scene = new Scene(location);
            scene.Entities = location.Seeds.Select(s => s.Hydrate(scene.Position)).ToList();
            return scene;
        }

        private static bool IsPartyInScene(IEnumerable<IEntity> party, Scene scene)
        {
            return scene.Players.Intersect(party).Any();
        }
    }
}
