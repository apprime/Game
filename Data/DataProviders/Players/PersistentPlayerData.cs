using Data.Models.Entities;
using Data.Models.Entities.Humans;

namespace Data.DataProviders.Players
{
    public class PersistentPlayerData : IPlayerDataProvider
    {
        public Player Add(Player player)
        {
            throw new System.NotImplementedException();
        }

        public Player Get(Id playerId, string connectionId)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
