using Core;
using Core.Routers;
using Data.Models.Entities.Humans;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Web.Hubs
{
    /// <summary>
    /// TODO: Handle erroneous request methods some way. IE misspelling the name of the invoked method should return "404" rather than crash
    /// </summary>
    public class PlayerHub : Hub
    {
        private IServiceProvider services;
        public PlayerHub(GameWrapper wrapper, IServiceProvider services)
        {
            this.services = services;
        }

        public override Task OnConnectedAsync()
        {
            return Clients.All.SendAsync("connected", "Yo has connected");
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return Clients.All.SendAsync("disconnected", "Yo has disconnected");
        }

        public void Subscribe(string eventString)
        {
            eventString = eventString + "/" + Context.ConnectionId;
            var e = EventParser.Parse(eventString, services);
            Engine.Instance.Push(e);
        }

        public void Unsubscribe(string eventString)
        {
            eventString = eventString + "/" + Context.ConnectionId;
            var e = EventParser.Parse(eventString, services);
            Engine.Instance.Push(e);
        }

        public void GetMyLocation(string eventString)
        {
            var e = EventParser.Parse(eventString, services);
            Engine.Instance.Push(e);
        }

        public void ChangeLocation(string eventString)
        {
            var e = EventParser.Parse(eventString, services);
            Engine.Instance.Push(e);
        }

        public void Attack(string eventString)
        {
            var e = EventParser.Parse(eventString, services);
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
