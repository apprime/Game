using Core.Processes.Events;
using Core.ResourceManagers;
using Data.Models.Entities.Humans;
using Data.Models.EventResolution;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Sockets;
using System.Collections.Generic;
using System.Linq;

namespace Web.Hubs
{
    public class GameWrapper
    {
        private IHubContext<PlayerHub> _hubContext;

        public GameWrapper(IHubContext<PlayerHub> hubContext)
        {
            _hubContext = hubContext;
            Event.EventResolved += Broadcast;
        }

        public void Broadcast(EventResult result)
        {
            IEnumerable<Player> targets = ResourceLocator.GetPlayers(result) ?? Enumerable.Empty<Player>();
            foreach (var p in targets.Where(t => t != null))
            {
                _hubContext.Clients
                           .Client(p.ConnectionId)
                           .InvokeAsync("Broadcast", result.ToJson());
            }
        }
    }
}
