using Data.DataProviders.Locations.Interfaces;
using Data.Models.Entities;
using Data.Models.Entities.EntityInterfaces;
using Data.Models.Nodes;
using System.Collections.Generic;

namespace Data.DataProviders.Locations
{
    public class MockedLocationData : IPositionDataProvider<Location>, IKnowParent<Sector>
    {
        //TODO: We need to be able to fetch list of location per sector(first 3 valiues in Position) from a resource.
        public Location Get(byte location)
        {
            switch(location)
            {
                case 1:
                    return GetTown();
                case 2:
                    return new Location("The Grass", Position.FromString("000000000002"));
                case 3:
                    return new Location("The Forest", Position.FromString("000000000003"));
                case 4:
                    return new Location("The Instance", Position.FromString("000000000004"));
                default:
                    //TODO: Handle default somehow
                    return new Location("Town", Position.FromString("000000000001"));
            }
        }

        private static Location GetTown()
        {
            var loc = new GlobalLocation("Town", Position.FromString("000000000001"));

            var seeds = new List<Seed>();
            seeds.Add(Seed.Monster(123));

            loc.Seeds = seeds;

            return loc;
        }

        public Sector GetParent(byte id)
        {
            throw new System.NotImplementedException();
        }
    }
}
