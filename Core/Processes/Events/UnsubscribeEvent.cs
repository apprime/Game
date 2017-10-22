using Core.ResourceManagers;
using Data.DataProviders.Players;
using Data.Models.Entities;
using Data.Models.Entities.Humans;
using Data.Models.EventResolution;
using Data.Repositories;

namespace Core.Processes.Events
{
    //Todo: This is trash. In order to properly remove a player, 
    // a) just extract them from the scene they are in and then remove
    // b) Mark them as offline, until all other players leaves scene, then remove
    internal class UnsubscribeEvent : Event
    {
        private Id _player;
        private Player _actor;
        private string _connectionInfo;

        #region Rules
        private const EventTargets _eventTargets = EventTargets.Player;
        #endregion

        public UnsubscribeEvent(string[] parts) : this(Id.FromString('P', parts[0]), parts[1]) { } //This CTOR only converts string array to real params.

        internal UnsubscribeEvent(Id player, string connectionInfo)
        {
            _player = player;
            _connectionInfo = connectionInfo;
        }

        protected override ReadonlyEvent GatherData()
        {
            var repo = new PlayerRepository(new MockedPlayerData());
            _actor = repo.Get(_player);
            return this;
        }

        protected override ReadonlyEvent Resolve()
        {
            if (_actor != null)
            {
                var delta = new Delta
                {
                    Actor = _actor,
                    Key = "Logout",
                    Value = SetLogoutMessage(),
                    Targets = ResourceLocator.GetPlayers(Result)
                };

                Result.Message = "You logged out";
                Result.Deltas.Add(delta);
                Result.Resolution = EventResolutionType.Commit;
            }
            else
            {
                Result.Resolution = EventResolutionType.Rollback;
            }

            Result.Actor = _actor;
            Result.Targets = _eventTargets;

            return this;
        }

        protected override Event Persist()
        {
            if(Result.Resolution == EventResolutionType.Commit)
            {
                _actor.LoggedOutPosition = _actor.Scene.Position;
                var repo = new PlayerRepository(new MockedPlayerData());
                repo.Unload(_actor);
                //TODO: Use SceneMutator to clear actor of scene.
                //TODO: Call PlayerRepo to save player data
            }

            return this;
        }

        private string SetLogoutMessage()
        {
            //Actor being null means they were not located
            if (_actor == null)
            {
                return "Already logged out";
            }
            else
            {
                return "Ok";
            }
        }
    }
}
