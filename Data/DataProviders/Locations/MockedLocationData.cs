using Data.DataProviders.Locations.Interfaces;
using Data.Models.Nodes;

namespace Data.DataProviders.Locations
{
    public class MockedLocationData : IPositionDataProvider<Location>, IKnowParent<Sector>
    {
        public Location Get(string locationId)
        {
            switch(locationId)
            {
                case "1111":
                    return new Location("Town", Position.FromString("1111"));
                default:
                    //TODO: Handle default somehow
                    return new Location("Town", Position.FromString("1111"));
            }
        }

        public Sector GetParent(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
