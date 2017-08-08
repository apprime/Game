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
            if (AffectGlobal(result))
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
            if (AffectSelf(result))
            {
                yield return (Player)result.Actor;
            }

            if (AffectParty(result))
            {
                Player player = (Player)result.Actor;
                foreach (Player p in player.Party)
                {
                    yield return p;
                }
            }

            if (AffectNearby(result))
            {
                Scene scene = result.Actor.Scene;
                foreach (Player p in scene.Players)
                {
                    yield return p;
                }
            }
        }

        /// <summary>
        /// This rule dictates that the event affects all players at a location
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private static bool AffectNearby(EventResult result)
        {
            return result.Targets.HasFlag(EventTargets.Nearby);
        }

        /// <summary>
        /// This rule dictates that an event affects all member of the party of caster
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private static bool AffectParty(EventResult result)
        {
            return result.Targets.HasFlag(EventTargets.Party);
        }

        /// <summary>
        /// This rule currently decides that event affects only the Actor herself.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private static bool AffectSelf(EventResult result)
        {
            return result.Targets.HasFlag(EventTargets.Player);
        }

        /// <summary>
        /// This rule decides that Event will affect All players currently connected
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private static bool AffectGlobal(EventResult result)
        {
            return result.Targets.HasFlag(EventTargets.World);
        }
    }
}
