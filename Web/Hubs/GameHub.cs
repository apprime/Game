﻿using Core;
using Core.Routers;
using Microsoft.AspNetCore.SignalR;

namespace Web.Hubs
{
    public class GameHub : Hub
    {
        public GameHub(GameWrapper wrapper)
        {
        }

        //TODO: We need a better separation of methods for event pushing.
        public void Push(string eventString)
        {
            eventString = eventString + ";" + Context.ConnectionId;
            var e = EventParser.Parse(eventString);
            Engine.Instance.Push(e);
        }
    }
}