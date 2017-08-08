using System;
using System.Collections.Generic;
using Core.Entites.Monsters;
using Core.Entities.Humans;
using Core.EventResolution;
using Core.Entities;

namespace Core.ResourceManagers
{
    //TODO: Make internal when client is no longer coupled
    public static class ResourceLocator
    {
        internal static void Add(Player p)
        {
            PlayerResources.Add(p);
        }

        internal static void Add(Monster monster)
        {
            MonsterResources.Add(monster);
        }

        internal static void Add(Scene scene)
        {
            SceneResources.Add(scene);
        }

        internal static void Remove(Player p)
        {
            PlayerResources.Remove(p);
        }

        //Todo: This should probably be replaced by simply taking the flag directly instead
        internal static IEnumerable<Player> GetPlayers(EventResult result)
        {
            return PlayerResources.Get(result);
        }

        //Todo: make internal when client is no longer coupled
        public static Player GetPlayer(string id)
        {
            return PlayerResources.Get(id);
        }

        public static object GetMonster(string id)
        {
            return MonsterResources.Get(id);
        }

        public static IEntity Get(string id)
        {
            return Get(Id.FromString(id));
        }

        private static Dictionary<char, Func<string, IEntity>> _get = new Dictionary<char, Func<string, IEntity>>
        {
            { 'M', MonsterResources.Get },
            {'P', PlayerResources.Get },
            //Add here
        };

        public static IEntity Get(Id id)
        {
            Func<string, IEntity> dataFetcher;
            if(!_get.TryGetValue(id.Prefix, out dataFetcher))
            {
                throw new ArgumentException("No resource is mapped to this prefix: " +id.Prefix);
            }

            return dataFetcher(id.Trunk);
        }
    }
}
