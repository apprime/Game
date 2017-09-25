using Data.Models.Exceptions;
using Core.Processes.Events;
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
                    return new Attack(parts);
                case "subscribe":
                    return new Subscribe(parts);
                case "unsubscribe":
                    return new Unsubscribe(parts);
                case "getMonster":
                    return new GetMonsterInfo(parts);
                default:
                    throw new ArgumentException("Wrong Message Format or Content");    
            }
        }
    }
}
