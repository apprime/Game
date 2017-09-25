using Core.Processes.Events;
using Core.ResourceManagers;
using Data.Models.Entities.Humans;
using Data.Models.EventResolution;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Web.Hubs
{
    public class GameWrapper
    {
        private IConnectionManager _manager;

        public GameWrapper(IConnectionManager manager)
        {
            _manager = manager;
            Event.EventResolved += Broadcast;
        }

        public void Broadcast(EventResult result)
        {
            IHubContext context = _manager.GetHubContext<Game>();

            IEnumerable<Player> targets = ResourceLocator.GetPlayers(result);
            foreach (var p in targets.Where(t => t != null))
            {
                context.Clients
                       .Client(p.ConnectionId)
                       .Broadcast(result.ToJson());
            }
        }
    }
}
