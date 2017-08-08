using Core.Entities;
using Core.Events;
using System;

namespace Core.EventResolution
{
    /// <summary>
    /// Currently the singleton for all game.
    /// TODO: Is this correct? Or should engine contain the resolver?
    /// </summary>
    public static class EventResolver
    {
        public static Event Resolve(string message)
        {
            //Todo: This should be replaced with JSON string handling:
            var parts = message.Split(';');

            //Parts1 should be Act: 
            switch(parts[0])
            {
                //Todo: Remember to remove this!
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
