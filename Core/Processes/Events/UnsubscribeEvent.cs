using Core.ResourceManagers;
using Data.Models.Entities;
using Data.Models.Entities.Humans;
using Data.Models.EventResolution;

namespace Core.Processes.Events
{
    //Todo: This is trash. In order to properly remove a player, 
    // a) just extract them from the scene they are in and then remove
    // b) Mark them as offline, until all other players leaves scene, then remove
    internal class UnsubscribeEvent : Event
    {
        private Id _player;
        private Player _actor;
        private bool _alreadyLoggedOut = false;

        #region Rules
        private const EventTargets _eventTargets = EventTargets.Player;
        #endregion

        public UnsubscribeEvent(string[] parts) : this(Id.FromString(parts[0])) { } //This CTOR only converts string array to real params.

        internal UnsubscribeEvent(Id player)
        {
            _player = player;
        }

        protected override ReadonlyEvent GatherData()
        {
            _actor = ResourceLocator.Get(_player) as Player;
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

                Result.Deltas.Add(delta);
                Result.Actor = _actor;
                Result.Targets = _eventTargets;
                Result.Resolution = EventResolutionType.Commit;
            }
            else
            {
                Result.Resolution = EventResolutionType.Rollback;
            }
            return this;
        }

        protected override Event Persist()
        {
            if(Result.Resolution == EventResolutionType.Commit)
            {
                _actor.LoggedOutPosition = _actor.Scene.Position;
                ResourceLocator.Remove(_actor);
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
