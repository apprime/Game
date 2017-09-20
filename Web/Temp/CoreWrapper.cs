using Core;
using Core.Entities.Humans;
using Core.EventResolution;
using Core.Events;
using Core.ResourceManagers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Infrastructure;
using System.Collections.Generic;

namespace Web.Temp
{
    public class CoreWrapper
    {
        public Dictionary<string, string> Players;
        private IConnectionManager _connectionManager { get; set; }
        private IHubContext _context;

        public CoreWrapper(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
            _context = _connectionManager.GetHubContext<GameHub>();
            Event.EventResolved += Broadcast;
            Players = new Dictionary<string, string>();

        }

        public void PushEvent(string eventString)
        {
            var e = EventParser.Parse(eventString);
            Engine.Instance.Push(e);
        }

        private void Broadcast(EventResult result)
        {
            IEnumerable<Player> targets = ResourceLocator.GetPlayers(result);
            foreach (var p in targets)
            {
                _context.Clients
                        .Client(p.ConnectionId)
                        .Broadcast(result.ToJson());
            }
        }
    }

    public class GameHub : Hub
    {

    }
}
