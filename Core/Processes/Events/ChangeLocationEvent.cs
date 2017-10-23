using Core.Mutators;
using Core.ResourceManagers;
using Data.DataProviders.Players;
using Data.Models.Entities;
using Data.Models.Entities.Humans;
using Data.Models.EventResolution;
using Data.Models.Nodes;
using Data.Repositories;

namespace Core.Processes.Events
{
    internal class ChangeLocationEvent : Event
    {
        private Id _playerId;
        private Position _destinationId;
        private Player _actor;

        #region Rules
        private const EventTargets _eventTargets = EventTargets.Player | EventTargets.Nearby | EventTargets.Party;
        private Movement movement;
        #endregion

        public ChangeLocationEvent(string[] parts) : this(Id.FromString('P',parts[0]), Position.FromString(parts[1])) { } //This CTOR only converts string array to real params.

        internal ChangeLocationEvent(Id player, Position destination)
        {
            _playerId = player;
            _destinationId = destination;
        }

        protected override ReadonlyEvent GatherData()
        {
            var repo = new PlayerRepository(new MockedPlayerData());
            _actor = repo.Get(_playerId);

            return this;
        }

        protected override ReadonlyEvent Resolve()
        {
            movement = new Movement();
            //TODO: Currently, the mutator is accessed in Resolve. It is supposed to only change things and therefor be accessed in Persist.
            //Setting of these values is not a write operation, but it involves logic that should not be inside the Process for Changing Location.
            //So thats a problem...
            SceneMutator.SetTraveler(_actor, movement);
            SceneMutator.SetOrigin(movement);
            SceneMutator.SetDestination(_destinationId, movement);

            //TODO: Currently, we detect login by saying that we have no origin, so it must be logon.
            //Maybe we should make this its own event?
            if(movement.Origin == null)
            {
                Result.Message = string.Format("{0} has logged into the location {1}", _actor.Name, movement.Destination.Name);
                Result.Deltas.Add(new Delta { Actor = _actor, Key = "PlayerLoggedIn", Value = movement.Destination.Name.ToString(), Targets = ResourceLocator.GetPlayers(Result) });
                Result.Resolution = EventResolutionType.Commit;
                return this;
            }

            if (CanGoToPosition(movement))
            {
                SetStandardResult();
            }
            else
            {
                Result.Message = string.Format("{0} is not allowed to go to {1} from this location.", _actor.Name, movement.Destination.Name); 
                Result.Resolution = EventResolutionType.Rollback;
            }

            return this;
        }

        private void SetStandardResult()
        {
            Result.Message = GenerateMessageString();
            Result.Deltas.Add(new Delta { Actor = _actor, Key = "PlayerMovingTo", Value = movement.Destination.Name.ToString(), Targets = ResourceLocator.GetPlayers(Result) });
            Result.Deltas.Add(new Delta { Actor = _actor, Key = "PlayerMovingFrom", Value = movement.Origin.Name.ToString(), Targets = ResourceLocator.GetPlayers(Result) });
            Result.Actor = movement.Traveler;
            Result.Targets = _eventTargets;
            Result.Resolution = EventResolutionType.Commit;
        }

        protected override Event Persist()
        {
            if (Result.Resolution == EventResolutionType.Commit)
            {
                SceneMutator.GoToPosition(movement);
                if(movement.Origin != null)
                {
                    SceneMutator.Cleanup(movement);
                }
                
                //TODO: New event to Player only, NewLocationEvent
            }

            return this;
        }

        private static bool CanGoToPosition(Movement movement)
        {
            return movement.Origin.Location.HasNeighbour(movement.Destination.Position); //TODO: This looks terrible, but doesn't break law of demeter. Not sure what to do about it.
        }

        //Todo: This is not very pretty, but somehow we must provide what actually happened to active entities in client
        private string GenerateMessageString()
        {
            return string.Format("{0} has moved from {1} to {2}", _actor.Name, _actor.Scene.Name, movement.Destination.Name);
        }
    }
}
