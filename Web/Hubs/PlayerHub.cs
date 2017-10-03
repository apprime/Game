using Core;
using Core.Routers;
using Data.Models.Entities.Humans;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Web.Hubs
{
    public class PlayerHub : Hub
    {
        public PlayerHub(GameWrapper wrapper)
        {

        }

        public override Task OnConnectedAsync()
        {
            return Clients.All.InvokeAsync("connected", "Yo has connected");
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return Clients.All.InvokeAsync("disconnected", "Yo has disconnected");
        }

        public void Subscribe(string eventString)
        {
            eventString = eventString + "/" + Context.ConnectionId;
            var e = EventParser.Parse(eventString);
            Engine.Instance.Push(e);
        }

        public void Unsubscribe(string eventString)
        {
            eventString = eventString + "/" + Context.ConnectionId;
            var e = EventParser.Parse(eventString);
            Engine.Instance.Push(e);
        }

        public void Create(Player player)
        {

        }

        public void Delete(Player player)
        {

        }
    }
}
