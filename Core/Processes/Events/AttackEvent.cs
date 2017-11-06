using Core.Mutators;
using Core.ResourceManagers;
using Data.Models.Entities;
using Data.Models.Entities.EntityInterfaces;
using Data.Models.EventResolution;
using Data.Repositories;

namespace Core.Processes.Events
{
    internal class AttackEvent : Event
    {
        private Id _attackerId;
        private Id _targetId;
        private IAttack _actor;
        private IDestructible _target;

        #region Rules
        private const EventTargets _eventTargets = EventTargets.Player | EventTargets.Nearby | EventTargets.Party;
        private Damage _damage;
        #endregion

        public AttackEvent(string[] parts) : this(Id.FromString('P', parts[0]), Id.FromString('M', parts[1])) { } //This CTOR only converts string array to real params.

        internal AttackEvent(Id attacker, Id target)
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
            Result.Actor = _actor;
            Result.Targets = _eventTargets;
            Result.Resolution = EventResolutionType.Commit;
            Result.Place = _actor.Location.Id;

            _damage = new Damage();
            CombatMutator.Setup(_damage);

            return this;
        }

        protected override Event Persist()
        {
            var repo = new PlayerRepository();

            CombatMutator.Attack(_actor, _damage);
            CombatMutator.Mitigate(_target, _damage);
            
            Result.Deltas.Add(new Delta { Actor = _actor, Key = "Attack", Value = GenerateAttackString(_damage), Targets = new IEntity[] { _target } });
            Result.Deltas.Add(new Delta { Actor = _actor, Key = "AttackMessage", Value = _damage.ToString(), Targets = repo.Get(Result) });

            CombatMutator.Cleanup(_damage);

            return this;
        }

        //Todo: This is not very pretty, but somehow we must provide what actually happened to active entities in client
        // Overload tostring on Damage? Other solution?
        private string GenerateAttackString(Damage damage)
        {
            return string.Format("{0};{1}", damage.Total.ToString(), damage.Target.Id.ToString());
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
