using Core.Entities.Humans;
using System.Collections.Generic;

namespace Core.Entities
{
    public  interface ILocation
    {
        IEnumerable<Player> GetPlayers(int instanceId);

    }
}