using Core.Entities;
using Core.EventResolution;
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

        internal override Event Dispatch()
        {
            Validate();

            _actor = ResourceLocator.Get(_attackerId) as IAttack;
            _target = ResourceLocator.Get(_targetId) as IDestructible;

            //Todo: This object doesn't feel natural. There should be a 
            // process inside Damage taking in both actor and target and 
            // the return should be the complete result.
            damage = new Damage();
            _actor.Attack(_target, damage);
            _target.Mitigate(_actor, damage);

            return this;
        }

        internal override Event Resolve()
        {
            //Todo: Make AttackMessage delta. Then make attack delta inculde value of Damage.Effective and Target Id so that client can adjust hp bar.
            Result.Deltas.Add(new Delta { Actor = _actor, Key = "Attack", Value = GenerateAttackString(damage), Targets = ResourceLocator.GetPlayers(Result) });
            Result.Deltas.Add(new Delta { Actor = _actor, Key = "AttackMessage", Value = damage.ToString(), Targets = ResourceLocator.GetPlayers(Result) });
            Result.Actor = _actor;
            Result.Targets = _eventTargets;
            Result.Resolution = EventResolutionType.Commit;

            return this;
        }

        internal override Event Persist()
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
