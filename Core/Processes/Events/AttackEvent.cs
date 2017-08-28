using Core.Entities;
using Core.EventResolution;
using Core.Mutators;
using Core.ResourceManagers;
using System;

namespace Core.Events
{
    internal class AttackEvent : Event
    {
        private Id _attackerId;
        private Id _targetId;
        private IAttack _actor;
        private IDestructible _target;
        private Damage damage;

        #region Rules
        private const EventTargets _eventTargets = EventTargets.Player | EventTargets.Nearby | EventTargets.Party;
        private Predicate<IDestructible> isDead = i => i.HitPoints.Current <= 0;
        #endregion

        public AttackEvent(string[] parts) : this(Id.FromString(parts[1]), Id.FromString(parts[2])) { } //This CTOR only converts string array to real params.

        internal AttackEvent(Id attacker, Id target)
        {
            _attackerId = attacker;
            _targetId = target;
        }

        protected override Event Dispatch()
        {
            Validate();

            _actor = ResourceLocator.Get(_attackerId) as IAttack;
            _target = ResourceLocator.Get(_targetId) as IDestructible;

            //Todo: This object doesn't feel natural. There should be a 
            // process inside Damage taking in both actor and target and 
            // the return should be the complete result.
            damage = Process(_actor, _target);

            return this;
        }

        protected override Event Resolve()
        {
            Result.Deltas.Add(new Delta { Actor = _actor, Key = "Attack", Value = GenerateAttackString(damage), Targets = new IEntity[] { _target } });
            Result.Deltas.Add(new Delta { Actor = _actor, Key = "AttackMessage", Value = damage.ToString(), Targets = ResourceLocator.GetPlayers(Result) });
            Result.Actor = _actor;
            Result.Targets = _eventTargets;
            Result.Resolution = EventResolutionType.Commit;

            return this;
        }

        protected override Event Persist()
        {
            _target.HitPoints.Current -= damage.Effective;

            if(isDead(_target))
            {
                _target.HitPoints.Current = 0;
                var killEvent = new KillEvent(_target.Id, _actor.Id);
                Engine.Instance.Push(killEvent);
            }

            return this;
        }

        /// <summary>
        /// Process for Attacking.
        /// </summary>
        private static Damage Process(IAttack actor, IDestructible target)
        {
            var damage = new Damage();
            Combat.Setup(damage);
            Combat.Attack(actor, damage);
            Combat.Mitigate(target, damage);
            Combat.Cleanup(damage);
            return damage;
        }

        //Todo: This is not very pretty, but somehow we must provide what actually happened to active entities in client
        // Overload tostring on Damage? Other solution?
        private string GenerateAttackString(Damage damage)
        {
            return string.Format("{0};{1}", damage.Effective.ToString(), damage.Target.Id);
        }

        private void Validate()
        {
            if (_actor == null)
            {
                //TODO: This attack is from a non-attacking entity, IE illegal move. Throw error!
            }
            if (_target == null)
            {
                //TODO: This attack is against a non destructible object, IE erroneous target. Return rollback "fizzle"?
            }
        }
    }
}
