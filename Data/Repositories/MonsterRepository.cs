using Data.DataProviders.Monsters;
using Data.Models.Entities;
using Data.Models.Entities.Monsters;
using Data.Models.Nodes;
using System.Collections.Generic;

namespace Data.Repositories
{
    //TODO: Figure out if repositories should know about active monsters or if we only allow locations to do that.
    public class MonsterRepository
    {
        private IMonsterDataProvider _dataProvider;

        //These are seeded Monsters, ie freshly made
        private Dictionary<int, Monster> _data = new Dictionary<int, Monster>();

        //These are all monsters active in all places
        private Dictionary<Id, Monster> _activeMonsters  = new Dictionary<Id, Monster>();

        public MonsterRepository(IMonsterDataProvider monsterDataProvider)
        {
            _dataProvider = monsterDataProvider;
        }

        public MonsterRepository()
        {
            _dataProvider = new MockedMonsterData();
        }

        public Monster Get(int id, Position position)
        {
            if(_data.TryGetValue(id, out Monster m))
            {
                return m;
            }
            else
            {
                var newMonster = _dataProvider.Get(id);
                newMonster.Id.Position = position;
                _data.Add(id, newMonster);
                _activeMonsters.Add(Id.FromParts('M', position, "123"), newMonster);
                return newMonster;
            }
        }

        public Monster Get(Id monsterId)
        {
            if (_activeMonsters.TryGetValue(monsterId, out Monster m))
            {
                return m;
            }
            else
            {
                var newMonster = _dataProvider.Get(int.Parse(monsterId.Trunk));
                _activeMonsters.Add(newMonster.Id, newMonster);
                return newMonster;
            }
        }
    }
}
