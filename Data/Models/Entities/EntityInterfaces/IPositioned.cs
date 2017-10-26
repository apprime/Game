using Data.Models.Nodes;
using Newtonsoft.Json;

namespace Data.Models.Entities.EntityInterfaces
{
    public interface IPositioned : IEntity
    {
        Position Position{ get; set; }
    }
}
