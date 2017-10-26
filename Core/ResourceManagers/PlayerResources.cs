using Data.Models.Entities.Humans;
using System.Collections.Concurrent;

namespace Core.ResourceManagers
{
    //Todo: Investigate if there should be a static resource manger or an instance that implements interface X
    public static class PlayerResources 
    {
        private static ConcurrentDictionary<string, Player> _players = new ConcurrentDictionary<string, Player>();



        //internal static Player aGet(string id)
        //{
        //    Player fetchedValue;
        //    if(!_players.TryGetValue(id, out fetchedValue))
        //    {
        //        return null;
        //    }

        //    return fetchedValue;
        //}

        internal static void Add(Player player)
        {
            //TODO: Handle when this doesnt work
            _players.TryAdd(player.Id.Trunk, player);
        }

        internal static void Remove(Player player)
        {
            Player playerValue;
            //TODO: Handle when this doesnt work
            _players.TryRemove(player.Id.Trunk, out playerValue);
        }

       
    }
}
