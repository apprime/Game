using Core;
using Core.Routers;
using Data.Models.Entities;
using Data.Models.Entities.Humans;
using Data.Models.Entities.Monsters;
using Microsoft.AspNetCore.SignalR;
using System;

namespace Web.Hubs
{
    public class MonsterHub : Hub
    {
        private readonly IServiceProvider serviceProvider;

        public MonsterHub(GameWrapper wrapper, IServiceProvider sp)
        {
            serviceProvider = sp;
        }

        public void GetInfo(string eventString)
        {
            eventString = eventString + "/" + Context.ConnectionId;
            var e = EventParser.Parse(eventString, serviceProvider);
            Engine.Instance.Push(e);
        }

        public void Loot(Id monsterId)
        {

        }

        public void Spawn(Monster monster)
        {

        }

        public void Attack(Player player)
        {
            //Deal with the fact that players may have blocking creatures or spells
        }
    }
}
