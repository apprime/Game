using Data.Models.Entities;
using Data.Models.Entities.EntityInterfaces;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
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
        public static IEntity Get(Id id, IServiceProvider sp)
        {
            switch (id.Prefix)
            {
                case 'P':
                    var playerRepo = sp.GetService<PlayerRepository>();
                    return playerRepo.Get(id);
                case 'M':
                    var monsterRepo = sp.GetService<MonsterRepository>();
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
