using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Events;

namespace Core.Entites.Monsters
{
    internal class Monster : IEntity, IActor, ILootable
    {
        #region TodoRegion

        //AIResource -> assign AI
        //Weapons -> Assign Weapons
        //Armour -> Assign armour

        #endregion

        public string Name { get; set; }

        public string Description { get; set; }

        public Id Id { get; protected set; }

        public Scene Scene { get; set; }

        public HitPoints HitPoints { get; set; }

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

        //Todo: This is where the monster decides what to do, should spawn a new event
        public int Act(IDestructible target)
        {
            var @event = new AttackEvent(Id, target.Id);
            Engine.Instance.Push(@event);
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