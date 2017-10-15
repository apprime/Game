using Core.Processes.Events;
using System;
using Data.Models.EventResolution;
using Data.Models.Exceptions;
using System.Collections.Generic;

namespace Core.Routers
{
    public static class EventParser
    {
        private static Dictionary<string, Func<string[], ReadonlyEvent>> PlayerEvents = new Dictionary<string, Func<string[], ReadonlyEvent>>
        {
            { "subscribe", s => new SubscribeEvent(s) },
            { "unsubscribe", s => new UnsubscribeEvent(s) }
        };

        private static Dictionary<string, Func<string[], ReadonlyEvent>> MonsterEvents = new Dictionary<string, Func<string[], ReadonlyEvent>>
        {
            { "getinfo", s => new GetMonsterInfo(s) },
        };

        private static Dictionary<string, Func<string[], ReadonlyEvent>> TodoEvents = new Dictionary<string, Func<string[], ReadonlyEvent>>
        {
            { "increasescore", s => new IncreaseScoreEvent(s) },
            { "attack", s => new Attack(s) }
        };

        private static Dictionary<EventCategory, Dictionary<string, Func<string[], ReadonlyEvent>>> Categories = new Dictionary<EventCategory, Dictionary<string, Func<string[], ReadonlyEvent>>>
        {
            {EventCategory.Player, PlayerEvents },
            {EventCategory.Monster, MonsterEvents },
            {EventCategory.None, TodoEvents }
        };

        public static ReadonlyEvent Parse(string message)
        {
            var separator = message.IndexOf('|');

            if (separator == -1)
            {
                throw new TodoException("Handle this erronenous input");
            }

            var header = message.Substring(0, separator);
            var tail = message.Substring(separator+1);

            return Parse(EventHeader.FromString(header), tail);
        }

        public static ReadonlyEvent Parse(EventHeader head, string tail)
        {
            var eventData = tail.Split('/');

            //This looks kinda wonky, but is O(n) and handles better than a switch.
            return Categories[head.Category]
                             [head.Action]
                             (eventData);
        }
    }
}
