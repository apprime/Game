using Data.Models.Entities.Humans;
using System.Collections.Generic;

namespace Data.Models.Entities
{
    public interface ILocation
    {
        IEnumerable<Player> GetPlayers(int instanceId);
    }
}