using Data.DataProviders.Locations;
using Data.Models.Entities.Humans;
using Data.Models.Gamestate;
using Data.Models.Nodes;
using Data.Repositories.Nodes;

namespace Core.Processes.ContentGeneration
{
    internal static class SceneFactory
    {
        internal static Scene GetOrCreate(Position position, Player player)
        {
            var repo = new LocationRepository(new MockedLocationData()); //TODO: Manage this in another way
            var location = repo.Get(position.Location);

            //Todo: Figure out if Repositories should be singletons, or stored in container or instanced each time.
            var candidate = (new SceneRepository()).Get(location, player.Party);
            if (candidate == null)
            {
                return (new SceneRepository()).Create(location);
            }
            else
            {
                return candidate;
            }
        }
    }
}
