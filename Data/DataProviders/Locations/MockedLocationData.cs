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
                    return GetForest(position);
                case var p when p == Position.FromNumbers(1, 1, 1, 4):
                    return GetCave(position);
                default:
                    //TODO: Handle default somehow
                    return GetTown(position);
            }
        }

        private static Location GetTown(Position position)
        {
            var seeds = new List<Seed>();
            seeds.Add(Seed.Monster(123));

            var loc = new GlobalLocation("Town", position, seeds);

            loc.Neighbours.Add(Position.FromString("001001001002"));
            loc.Neighbours.Add(Position.FromString("001001001003"));

            loc.ImageUrl = "town.png";

            return loc;
        }

        private static Location GetGrass(Position position)
        {
            var loc = new Location("The Grass", position);

            loc.Seeds.Add(Seed.Monster(123));

            var repo = new MonsterRepository();
            loc.Entities.Add(repo.Get(123, loc));

            loc.Neighbours.Add(Position.FromString("001001001001"));
            loc.Neighbours.Add(Position.FromString("001001001004"));

            loc.ImageUrl = "grass.png";

            return loc;
        }

        private Location GetForest(Position position)
        {
            var loc = new Location("The Forest", position);

            var repo = new MonsterRepository();
            loc.Entities.Add(repo.Get(123, loc));

            loc.Neighbours.Add(Position.FromString("001001001001"));
            loc.Neighbours.Add(Position.FromString("001001001004"));

            loc.ImageUrl = "forest.png";

            return loc;
        }

        private Location GetCave(Position position)
        {
            var loc = new Location("The Cave", position);

            var repo = new MonsterRepository();
            loc.Entities.Add(repo.Get(123, loc));
            loc.Entities.Add(repo.Get(124, loc));

            loc.Neighbours.Add(Position.FromString("001001001002"));
            loc.Neighbours.Add(Position.FromString("001001001003"));

            loc.ImageUrl = "cave.png";

            return loc;
        }

        public Sector GetParent(byte id)
        {
            throw new System.NotImplementedException();
        }
    }
}
