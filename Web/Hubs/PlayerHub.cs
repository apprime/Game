using Data.Models.Entities.Humans;
using Microsoft.AspNetCore.SignalR;

namespace Web.Hubs
{
    public class PlayerHub : Hub
    {
        public PlayerHub(GameWrapper wrapper)
        {

        }

        public void Subscribe(Player player)
        {

        }

        public void Unsubscribe(Player player)
        {

        }

        public void Create(Player player)
        {

        }

        public void Delete(Player player)
        {

        }
    }
}
