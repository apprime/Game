using Data.Models.Entities;
using Data.Models.Entities.EntityInterfaces;
using Data.Repositories;
using Data.Repositories.Nodes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.ResourceManagers
{
    //TODO: We are trying to move away from using this and into using only Repositories


    //TODO Locator should?:
    // a) Always use Position/Id to get Location if possible. Then search all Entities there
    // b) Go through all Repos one by one if needs be
    // c) Provide Repos?
    public static class ResourceLocator
    {
        //public static void Add(Player p)
        //{
        //    PlayerResources.Add(p);
        //}

        //public static void Add(Monster monster)
        //{
        //    MonsterResources.Add(monster);
        //}

        //public static void Add(Scene scene)
        //{
        //    SceneResources.Add(scene);
        //}

        //public static void Remove(Player p)
        //{
        //    PlayerResources.Remove(p);
        //}

        ////Todo: This should probably be replaced by simply taking the flag directly instead
        //public static IEnumerable<Player> GetPlayers(EventResult result)
        //{
        //    return PlayerResources.Get(result);
        //}

        ////Todo: make internal when client is no longer coupled
        ////public static Player GetPlayer(string id)
        ////{
        ////    return PlayerResources.Get(id);
        ////}

        //public static Monster GetMonster(string id)
        //{
        //    return MonsterResources.Get(id);
        //}

        //public static IEntity Get(string id)
        //{
        //    return Get(Id.FromString(id));
        //}

        //private static Dictionary<char, Func<string, IEntity>> _get = new Dictionary<char, Func<string, IEntity>>
        //{
        //    {'M', MonsterResources.Get },
        //    //{'P', PlayerResources.Get },
        //    {'L', (s) => (new LocationRepository()).Get(s) }
        //    //Add here
        //};

        public static async Task<IEntity> Get(Id id)
        {
            switch(id.Prefix)
            {
                case 'P':
                    var playerRepo = new PlayerRepository();
                    return await playerRepo.Get(id);
                case 'M':
                    var monsterRepo = new MonsterRepository();
                    return monsterRepo.Get(id);
                //case 'L':
                //    var locationRepo = new LocationRepository();
                //    return locationRepo.Get(id.Position);
                default:
                    throw new ArgumentException("No resource is mapped to this prefix: " + id.Prefix);
            }
        }
    }
}
