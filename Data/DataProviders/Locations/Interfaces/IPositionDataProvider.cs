using Data.Models.Nodes;
using System.Threading.Tasks;

namespace Data.DataProviders.Locations
{
    public interface IPositionDataProvider<T>
    {
        Task<T> Get(Position pos);
    }
}
