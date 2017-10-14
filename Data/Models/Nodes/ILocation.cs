using Data.Models.Entities.Humans;
using System.Collections.Generic;

namespace Data.Models.Nodes
{
    public interface ILocation
    {
        IEnumerable<Player> GetPlayers(int instanceId);
    }
}