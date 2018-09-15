using Core.Processes.Events;
using System;
using Data.Models.EventResolution;
using Data.Models.Exceptions;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Routers
{
    public static class EventParser
    {
        private static Dictionary<string, Func<string[], IServiceProvider, ReadonlyEvent>> PlayerEvents = new Dictionary<string, Func<string[], IServiceProvider, ReadonlyEvent>>
        {
            { "subscribe", (s, injector) => new SubscribeEvent(s, injector) },
            { "unsubscribe", (s, injector) => new UnsubscribeEvent(s, injector) },
            { "getmyposition", (s, injector) => new GetMyLocationEvent(s, injector) },
            { "changelocation",( s, injector) => new EnterLocationEvent(s, injector) },
            { "attack", (s, injector) => new AttackEvent(s, injector) }
        };

        private static Dictionary<string, Func<string[], IServiceProvider, ReadonlyEvent>> MonsterEvents = new Dictionary<string, Func<string[], IServiceProvider, ReadonlyEvent>>
        {
            { "getinfo", (s, injector) => new GetMonsterInfo(s, injector) },
        };

        private static Dictionary<string, Func<string[], IServiceProvider, ReadonlyEvent>> TodoEvents = new Dictionary<string, Func<string[], IServiceProvider, ReadonlyEvent>>
        {
            { "increasescore", (s, injector) => new IncreaseScoreEvent(s, injector) },
            { "attack", (s, injector) => new AttackEvent(s, injector) }
        };

        private static Dictionary<EventCategory, Dictionary<string, Func<string[], IServiceProvider, ReadonlyEvent>>> Categories = new Dictionary<EventCategory, Dictionary<string, Func<string[], IServiceProvider, ReadonlyEvent>>>
        {
            {EventCategory.Player, PlayerEvents },
            {EventCategory.Monster, MonsterEvents },
            {EventCategory.None, TodoEvents }
        };

        public static ReadonlyEvent Parse(string message, IServiceProvider service)
        {
            var separator = message.IndexOf('|');

            if (separator == -1)
            {
                throw new TodoException("Handle this erronenous input");
            }

            var header = message.Substring(0, separator);
            var tail = message.Substring(separator+1);

            return Parse(EventHeader.FromString(header), tail, service);
        }

        public static ReadonlyEvent Parse(EventHeader head, string tail, IServiceProvider service)
        {
            var eventData = tail.Split('/');

            //This looks kinda wonky, but is O(n) and handles better than a switch.
            return Categories[head.Category]
                             [head.Action]
                             (eventData, service);
        }
    }
}
