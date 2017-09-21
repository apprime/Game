using Data.Models.Entities.Monsters;
using System.Collections.Concurrent;

namespace Core.ResourceManagers
{
    internal static class MonsterResources
    {
        private static ConcurrentDictionary<string, Monster> _monsters = new ConcurrentDictionary<string, Monster>();

        internal static Monster Get(string id)
        {
            Monster monster;
            _monsters.TryGetValue(id, out monster);
            return monster;
        }

        internal static void Add(Monster monster)
        {
            //TODO: Handle when this doesnt work
            _monsters.TryAdd(monster.Id.Trunk, monster);
        }

        internal static void Remove(Monster monster)
        {
            _monsters.TryRemove(monster.Id.Trunk, out monster);
        }
    }
}
