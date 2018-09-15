using Core;
using Core.Routers;
using Microsoft.AspNetCore.SignalR;
using System;

namespace Web.Hubs
{
    public class GameHub : Hub
    {
        private readonly IServiceProvider serviceProvider;

        public GameHub(GameWrapper wrapper, IServiceProvider sp)
        {
            serviceProvider = sp;
        }

        //TODO: We need a better separation of methods for event pushing.
        public void Push(string eventString)
        {
            eventString = eventString + ";" + Context.ConnectionId;
            var e = EventParser.Parse(eventString, serviceProvider);
            Engine.Instance.Push(e);
        }
    }
}
