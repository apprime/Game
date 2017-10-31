using Data.Models.Nodes;

namespace Data.Models.Entities.EntityInterfaces
{
    public interface IPositioned : IEntity
    {
        Location Location{ get; set; }
    }
}
