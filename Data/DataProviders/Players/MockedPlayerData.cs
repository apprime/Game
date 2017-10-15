using Data.Models.Entities;
using Data.Models.Entities.Humans;

namespace Data.DataProviders.Players 
{
    public class MockedPlayerData : IPlayerDataProvider
    {
        public Player Add(Player player)
        {
            throw new System.NotImplementedException();
        }

        public Player Get(Id playerId, string connectionId)
        {
            return new Player("P1111123/PlayerMcPlayerface/1/" + connectionId);
        }

        public void Remove(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
