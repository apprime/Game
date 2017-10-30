using Data.DataProviders.Locations.Interfaces;
using Data.Models.Entities;
using Data.Models.Nodes;
using Data.Repositories;
using System.Collections.Generic;

namespace Data.DataProviders.Locations
{
    public class MockedLocationData : IPositionDataProvider<Location>, IKnowParent<Sector>
    {
        //TODO: We need to be able to fetch list of location per sector(first 3 valiues in Position) from a resource.
        public Location Get(Position position)
        {
            switch (position)
            {
                case var p when p == Position.FromNumbers(1, 1, 1, 1):
                    return GetTown(position);
                case var p when p == Position.FromNumbers(1, 1, 1, 2):
                    return GetGrass(position);
                case var p when p == Position.FromNumbers(1, 1, 1, 3):
                    return new Location("The Forest", Position.FromString("000000000003"));
                case var p when p == Position.FromNumbers(1, 1, 1, 4):
                    return new Location("The Instance", Position.FromString("000000000004"));
                default:
                    //TODO: Handle default somehow
                    return GetTown(position);
            }
        }

        private static Location GetGrass(Position position)
        {
            var loc = new Location("The Grass", Position.FromString("001001001002"));

            loc.Seeds.Add(Seed.Monster(123));

            var repo = new MonsterRepository();
            loc.Entities.Add(repo.Get(123, position));

            loc.Neighbours.Add(Position.FromString("001001001001"));
            loc.Neighbours.Add(Position.FromString("001001001004"));

            return loc;
        }

        private static Location GetTown(Position position)
        {
            var seeds = new List<Seed>();
            seeds.Add(Seed.Monster(123));

            var loc = new GlobalLocation("Town", Position.FromString("001001001001"), seeds);

            loc.Neighbours.Add(Position.FromString("001001001002"));
            loc.Neighbours.Add(Position.FromString("001001001003"));

            return loc;
        }

        public Sector GetParent(byte id)
        {
            throw new System.NotImplementedException();
        }
    }
}
