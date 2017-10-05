﻿using Core.Processes.Events;
using Data.Models.Entities;
using Data.Models.Entities.EntityInterfaces;
using Data.Models.Exceptions;
using System;


namespace Core.Mutators
{
    /// <summary>
    /// Handles mutation of combat related actions.
    /// </summary>
    public static class Combat
    {
        private static Predicate<IDestructible> isDead = i => i.HitPoints.Current <= 0;

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
            defender.HitPoints.Current -= payload.Effective;
            return payload;
        }

        internal static void Setup(Damage damage)
        {
            throw new NotImplementedException();
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
