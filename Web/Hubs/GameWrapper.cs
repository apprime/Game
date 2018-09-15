using Core.Processes.Events;
using Core.ResourceManagers;
using Data.Models.Entities.Humans;
using Data.Models.EventResolution;
using Data.Repositories;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Data.DataProviders;
using Data.DataProviders.Players;

namespace Web.Hubs
{
    /// <summary>
    /// At this point, we always push events to clients using the Broadcast event
    /// This means we do not distinguish from which hub the event is pushed.
    /// Maybe that is not so smart, but so far it works.
    /// 
    /// Wrapper is a singleton that registers with webserver, it exists to ensure
    /// we don't push duplicate messages to clients since hubs are not static.
    /// </summary>
    public class GameWrapper
    {
        private readonly PlayerRepository playerRepository;
        private IHubContext<PlayerHub> _hubContext;

        public GameWrapper(IHubContext<PlayerHub> hubContext, IServiceScopeFactory scopeFactory)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<GameDataContext>();
                playerRepository = new PlayerRepository(new PersistentPlayerData(dbContext));
            }
        
            _hubContext = hubContext;
            Event.EventResolved += Broadcast;
        }

        public void Broadcast(EventResult result)
        {
            IEnumerable<Player> targets = playerRepository.Get(result) ?? Enumerable.Empty<Player>();
            foreach (var p in targets.Where(t => t != null))
            {
                _hubContext.Clients
                           .Client(p.ConnectionId)
                           .SendAsync("broadcast", result.ToJson());
                            //TODO: Question, how do we keep track of client side eventhandlers? Broadcast only exists in stringly format here
            }
        }
    }
}
