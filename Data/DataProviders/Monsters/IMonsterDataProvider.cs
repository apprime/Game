using Data.Models.Entities.Monsters;

namespace Data.DataProviders.Monsters
{
    public interface IMonsterDataProvider
    {
        //Monster Add(Monster monster);
        Monster Get(int monsterId);
        //void Remove(Monster monster);
    }
}
