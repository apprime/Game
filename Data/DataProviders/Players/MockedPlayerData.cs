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
            var p = new Player("P001001001001123/PlayerMcPlayerface/1/" + connectionId)
            {
                ImageUrl = "player.png"
            };

            return p;
        }

        public void Remove(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
