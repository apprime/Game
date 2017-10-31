using Core.Processes.Events;
using Data.Models.Entities;
using Data.Models.Entities.EntityInterfaces;
using System;


namespace Core.Mutators
{
    /// <summary>
    /// Handles mutation of combat related actions.
    /// </summary>
    public static class CombatMutator
    {
        private static Predicate<IDestructible> isDead = i => i.HitPoints.Current <= 0;

        internal static void Setup(Damage damage)
        {
            //throw new NotImplementedException();
        }

        public static Damage Attack(IAttack attacker, Damage payload)
        {
            payload.Actor = attacker;
            payload.Total = attacker.Damage;
 
            payload.DamageType = attacker.DamageType.ToString("G");
            payload.Name = attacker.AttackName;

            return payload;
        }

        public static Damage Mitigate(IDestructible defender, Damage payload)
        {
            payload.Target = defender;
            //payload.Effective = attacker.Damage;//TOdo: This is subject to change when armor and stuff
            defender.HitPoints.Current -= payload.Total;
            return payload;
        }

        internal static void Cleanup(Damage damage)
        {
            var poorTarget = damage.Target;
            if (isDead(poorTarget))
            {
                poorTarget.HitPoints.Current = 0;
                var killEvent = new KillEvent(poorTarget.Id, damage.Actor.Id);
                Engine.Instance.Push(killEvent);
            }
        }
    }
}
