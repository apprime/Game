using Data.DataProviders.Monsters;
using Data.Models.Entities;
using Data.Models.Entities.Monsters;
using System.Collections.Generic;

namespace Data.Repositories
{
    public class MonsterRepository
    {
        private IMonsterDataProvider _dataProvider;
        private Dictionary<int, Monster> _data = new Dictionary<int, Monster>();


        public MonsterRepository(IMonsterDataProvider monsterDataProvider)
        {
            _dataProvider = monsterDataProvider;
        }

        public MonsterRepository()
        {
            _dataProvider = new MockedMonsterData();
        }

        public Monster Get(Seed monster)
        {
            if(_data.TryGetValue(monster.Id, out Monster m))
            {
                return m;
            }
            else
            {
                var newMonster = _dataProvider.Get(monster);
                _data.Add(monster.Id, newMonster);
                return newMonster;
            }
        }
    }
}
