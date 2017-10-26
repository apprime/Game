using Data.Models.Entities;
using Data.Models.EventResolution;
using Core.Mutators;
using Core.ResourceManagers;
using System;
using Data.Models.Entities.EntityInterfaces;
using Data.Repositories;

namespace Core.Processes.Events
{
    internal class Attack : Event
    {
        private Id _attackerId;
        private Id _targetId;
        private IAttack _actor;
        private IDestructible _target;
        private Damage damage;

        #region Rules
        private const EventTargets _eventTargets = EventTargets.Player | EventTargets.Nearby | EventTargets.Party;
        #endregion

        public Attack(string[] parts) : this(Id.FromString(parts[1]), Id.FromString(parts[2])) { } //This CTOR only converts string array to real params.

        internal Attack(Id attacker, Id target)
        {
            _attackerId = attacker;
            _targetId = target;
        }

        protected override ReadonlyEvent GatherData()
        {
            Validate();

            _actor = ResourceLocator.Get(_attackerId) as IAttack;
            _target = ResourceLocator.Get(_targetId) as IDestructible;

            return this;
        }

        protected override ReadonlyEvent Resolve()
        {
            var repo = new PlayerRepository();

            Result.Deltas.Add(new Delta { Actor = _actor, Key = "Attack", Value = GenerateAttackString(damage), Targets = new IEntity[] { _target } });
            Result.Deltas.Add(new Delta { Actor = _actor, Key = "AttackMessage", Value = damage.ToString(), Targets = repo.Get(Result) });
            Result.Actor = _actor;
            Result.Targets = _eventTargets;
            Result.Resolution = EventResolutionType.Commit;

            return this;
        }

        protected override Event Persist()
        {
            damage = Process(_actor, _target);
            return this;
        }

        /// <summary>
        /// Process for Attacking.
        /// </summary>
        private static Damage Process(IAttack actor, IDestructible target)
        {
            var damage = new Damage();
            CombatMutator.Setup(damage);
            CombatMutator.Attack(actor, damage);
            CombatMutator.Mitigate(target, damage);
            CombatMutator.Cleanup(damage);
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
