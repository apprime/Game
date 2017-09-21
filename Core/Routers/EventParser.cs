using Data.Models.Exceptions;
using Core.Events;
using System;

namespace Core.Routers
{
    public static class EventParser
    {
        public static Event Parse(string message)
        {
            //Todo: We expect some sort of message from front. TBD how to parse it.
            var parts = message.Split(';');

            //Parts1 should be Act: 
            switch(parts[0])
            {
                //Todo: Remember to remove this event!
                case "increaseScore":
                    return new IncreaseScoreEvent(parts);
                case "attack":
                    return new Subscribe(parts);
                case "subscribe":
                    //TODO: Subscribe (player)
                    throw new TodoException("Player cannot join the game server yet");
                case "unsubscribe":
                    //TODO: Unsubscribe (player)
                    throw new TodoException("Players cannot leave the game server yet (mohaha)");
                default:
                    throw new ArgumentException("Wrong Message Format or Content");    
            }
        }
    }
}
