using Core.Events;
using System;

namespace Core.EventResolution
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
                case "IncreaseScore":
                    return new IncreaseScoreEvent(parts);
                case "Attack":
                    return new AttackEvent(parts);
                default:
                    throw new ArgumentException("Wrong Message Format or Content");    
            }
        }
    }
}
