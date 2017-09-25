using Data.Models.Gamestate;
using Newtonsoft.Json;

namespace Data.Models.Entities.EntityInterfaces
{
    public interface IPositioned : IEntity
    {
        [JsonIgnore]
        Scene Scene { get; set; }
    }
}
