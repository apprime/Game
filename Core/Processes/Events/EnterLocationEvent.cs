using Core.Mutators;
using Data.Models.Entities;
using Data.Models.Entities.Humans;
using Data.Models.EventResolution;
using Data.Models.Nodes;
using Data.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Processes.Events
{
    internal class EnterLocationEvent : Event
    {
        private Id _playerId;
        private Position _destinationId;
        private Player _actor;

        #region Rules
        private const EventTargets _eventTargets = EventTargets.Player | EventTargets.Nearby;
        private Movement movement;
        #endregion

        private PlayerRepository repo;
        private IServiceProvider serviceProvider;

        public EnterLocationEvent(string[] parts, IServiceProvider sp) : this(Id.FromString('P',parts[0]), Position.FromString(parts[1]))
        {
            this.serviceProvider = sp;
            repo = sp.GetService<PlayerRepository>();
        } //This CTOR only converts string array to real params.

        internal EnterLocationEvent(Id player, Position destination)
        {
            _playerId = player;
            _destinationId = destination;
        }

        protected override ReadonlyEvent GatherData()
        {
            _actor = repo.Get(_playerId);

            return this;
        }

        protected override ReadonlyEvent Resolve()
        {
            movement = new Movement();
            //TODO: Currently, the mutator is accessed in Resolve. It is supposed to only change things and therefor be accessed in Persist.
            //Setting of these values is not a write operation, but it involves logic that should not be inside the Process for Changing Location.
            //So thats a problem...
            LocationMutator.SetTraveler(_actor, movement);
            LocationMutator.SetOrigin(movement);
            LocationMutator.SetDestination(_destinationId, movement);

            //TODO: Currently, we detect login by saying that we have no origin, so it must be logon.
            //Maybe we should make this its own event?
            if (movement.Origin == null)
            {
                Result.Message = string.Format("{0} has logged into the location {1}", _actor.Name, movement.Destination.Name);
                Result.Deltas.Add(new Delta { Actor = _actor, Key = "PlayerLoggedIn", Value = movement.Destination.Name.ToString(), Targets = repo.Get(Result) });
                SetStandardResult();
                return this;
            }

            if (CanGoToPosition(movement))
            {
                SetStandardResult();
                Result.Message = GenerateMessageString();
            }
            else
            {
                movement.Destination.RemovePlayer(movement.Traveler);
                Result.Message = string.Format("{0} is not allowed to go to {1} from this location.", _actor.Name, movement.Destination.Name); 
                Result.Resolution = EventResolutionType.Rollback;
            }

            return this;
        }

        private void SetStandardResult()
        {
            Result.Actor = movement.Traveler;
            Result.Place = movement.Destination.Id;
            Result.Targets = _eventTargets;
            Result.Resolution = EventResolutionType.Commit;
        }

        protected override Event Persist()
        {
            if (Result.Resolution != EventResolutionType.Commit)
            {
                return this;
            }

            Result.Deltas.Add(new Delta { Actor = _actor, Key = "NewBackground", Value = movement.Destination.ImageUrl, Targets = repo.Get(Result) });
            LocationMutator.GoToPosition(movement);
            if (movement.Origin == null)
            {
                return this;
            }

            LocationMutator.RemovePlayerFromScene(movement);

            if (!movement.Origin.Players.Any())
            {
                LocationMutator.RemoveEmptyScene(movement);
            }
            else
            {
                var e = new LeaveLocationEvent(movement.Traveler, movement.Origin.Id, serviceProvider);
                Engine.Instance.Push(e);
            }

            return this;
        }

        private static bool CanGoToPosition(Movement movement)
        {
            return movement.Origin.HasNeighbour(movement.Destination.Position); //TODO: This looks terrible, but doesn't break law of demeter. Not sure what to do about it.
        }

        //Todo: This is not very pretty, but somehow we must provide what actually happened to active entities in client
        private string GenerateMessageString()
        {
            return string.Format("{0} has moved from {1} to {2}", _actor.Name, movement.Origin.Name, movement.Destination.Name);
        }
    }
}
