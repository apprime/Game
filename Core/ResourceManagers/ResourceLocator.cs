using System;
using System.Collections.Generic;
using Data.Models.Entities.Monsters;
using Data.Models.Entities.Humans;
using Data.Models.Gamestate;
using Data.Models.EventResolution;
using Data.Models.Entities;

namespace Core.ResourceManagers
{
    //This is public because Web uses GetPlayers to ..get players
    //We should have a property in EventResult that does this insead
    public static class ResourceLocator
    {
        public static void Add(Player p)
        {
            PlayerResources.Add(p);
        }

        public static void Add(Monster monster)
        {
            MonsterResources.Add(monster);
        }

        public static void Add(Scene scene)
        {
            SceneResources.Add(scene);
        }

        public static void Remove(Player p)
        {
            PlayerResources.Remove(p);
        }

        //Todo: This should probably be replaced by simply taking the flag directly instead
        public static IEnumerable<Player> GetPlayers(EventResult result)
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
