using Core;
using Core.Routers;
using Data.Models.Entities;
using Data.Models.Entities.Humans;
using Data.Models.Entities.Monsters;
using Microsoft.AspNetCore.SignalR;

namespace Web.Hubs
{
    public class MonsterHub : Hub
    {
        public MonsterHub(GameWrapper wrapper)
        {

        }

        public void GetInfo(string eventString)
        {
            eventString = eventString + "/" + Context.ConnectionId;
            var e = EventParser.Parse(eventString);
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
