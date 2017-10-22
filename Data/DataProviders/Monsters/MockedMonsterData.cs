using Data.Models.Entities;
using Data.Models.Entities.Monsters;
using System;

namespace Data.DataProviders.Monsters
{
    public class MockedMonsterData : IMonsterDataProvider
    {
        public Monster Get(Seed monster)
        {
            if(monster.Id == 123)
            {
                return new AdmiralAardwark();
            }

            throw new NotImplementedException();
        }

        //public void Remove(Monster monster)
        //{
        //    throw new NotImplementedException();
        //}

        //public Monster Add(Monster monster)
        //{
        //    throw new NotImplementedException();
        //}
    }
}