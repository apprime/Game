using Data.Models.Nodes;

namespace Data.DataProviders.Locations
{
    public interface IPositionDataProvider<T>
    {
        T Get(Position pos);
    }
}
