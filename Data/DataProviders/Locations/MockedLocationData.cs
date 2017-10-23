using Data.DataProviders.Locations.Interfaces;
using Data.Models.Entities;
using Data.Models.Nodes;
using System.Collections.Generic;

namespace Data.DataProviders.Locations
{
    public class MockedLocationData : IPositionDataProvider<Location>, IKnowParent<Sector>
    {
        //TODO: We need to be able to fetch list of location per sector(first 3 valiues in Position) from a resource.
        public Location Get(byte location)
        {
            switch (location)
            {
                case 1:
                    return GetTown();
                case 2:
                    return GetGrass();
                case 3:
                    return new Location("The Forest", Position.FromString("000000000003"));
                case 4:
                    return new Location("The Instance", Position.FromString("000000000004"));
                default:
                    //TODO: Handle default somehow
                    return GetTown();
            }
        }

        private static Location GetGrass()
        {
            var loc = new Location("The Grass", Position.FromString("001001001002"));

            loc.Seeds.Add(Seed.Monster(123));
            loc.Neighbours.Add(Position.FromString("001001001001"));
            loc.Neighbours.Add(Position.FromString("001001001004"));

            return loc;
        }

        private static Location GetTown()
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
