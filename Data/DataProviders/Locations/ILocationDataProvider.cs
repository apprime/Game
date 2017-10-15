using Data.Models.Nodes;

namespace Data.DataProviders.Locations
{
    public interface ILocationDataProvider
    {
        Location Get(string locationId);
    }
}
