using Core.Entities;
using Core.Entities.Humans;
using Core.EventResolution;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Core.ResourceManagers
{
    //Todo: Investigate if there should be a static resource manger or an instance that implements interface X
    public static class PlayerResources 
    {
        private static ConcurrentDictionary<string, Player> _players = new ConcurrentDictionary<string, Player>();

        internal static IEnumerable<Player> Get(EventResult result)
        {
            if (result.Targets.Contains(EventTargets.World))
            {
                return _players.Select(i => i.Value);
            }
            else
            {
                return GetFiltered(result).Distinct();
            }
        }

        internal static Player Get(string id)
        {
            return _players[id];
        }

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

        private static IEnumerable<Player> GetFiltered(EventResult result)
        {
            if (result.Targets.Contains(EventTargets.Player))
            {
                yield return (Player)result.Actor;
            }

            if (result.Targets.Contains(EventTargets.Party))
            {
                Player player = (Player)result.Actor;
                foreach (Player p in player.Party)
                {
                    yield return p;
                }
            }

            if (result.Targets.Contains(EventTargets.Nearby))
            {
                Scene scene = result.Actor.Scene;
                foreach (Player p in scene.Players)
                {
                    yield return p;
                }
            }
        }
    }
}
