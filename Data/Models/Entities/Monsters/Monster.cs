using Data.Models.Entities.EntityInterfaces;
using Data.Models.Nodes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Data.Models.Entities.Monsters
{
    [JsonObject]
    public class Monster : IPositioned, IActor, ILootable
    {
        #region TodoRegion

        //AIResource -> assign AI
        //Weapons -> Assign Weapons
        //Armour -> Assign armour

        #endregion

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public string Description { get; set; }

        [JsonProperty]
        public Id Id { get; protected set; }

        public Location Location{ get; set; }

        [JsonProperty]
        public HitPoints HitPoints { get; set; }       

        public int Damage => 0;                                 //TODO

        public string AttackName => "Todo";                     //TODO

        public DamageType DamageType => DamageType.Physical;    //TODO

        public virtual Damage Attack(IDestructible target, Damage payload)
        {
            //1. Calc damage
            //TODO: There should be a range of damage
            const int cDamage = 1;
            payload.Total = cDamage;
            return payload;
        }

        public virtual Damage Mitigate(IAttack attacker, Damage payload)
        {
            //armor etc. changes the amount of damage taken
            //but we dont have that yet because TODO TODO TODO;
            payload.Effective = payload.Total - 1;

            return payload;
        }

        //Todo: This is where the monster decides what to do -> leads to event
        public int Act(/*TODO*/)
        {
            //TODO: We need an AI routine that ticks in the engine.
            //      It needs to find all IActor that are NPCS and give them orders.
            //      Save orders inside each instance object to speed things up. (State machine?)
            //var @event = new Attack(Id, target.Id);
            //Engine.Instance.Push(@event);
            return 1; //TODO
        }

        //TODO: Placeholder
        public virtual IEnumerable<Damage> Attack(IEnumerable<IDestructible> targets, Damage payload)
        {
            foreach(var t in targets)
            {
                yield return Attack(t, new Damage());
            }
        }

        public virtual Loot Yield(IActor actor)
        {
            return new Loot();
        }
    }
}