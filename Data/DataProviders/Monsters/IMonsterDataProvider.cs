using Data.Models.Entities;
using Data.Models.Entities.Monsters;

namespace Data.DataProviders.Monsters
{
    public interface IMonsterDataProvider
    {
        //Monster Add(Monster monster);
        Monster Get(Seed monsterId);
        //void Remove(Monster monster);
    }
}
