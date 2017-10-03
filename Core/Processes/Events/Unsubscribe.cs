using Core.ResourceManagers;
using Data.Models.Entities;
using Data.Models.Entities.Humans;
using Data.Models.EventResolution;

namespace Core.Processes.Events
{
    //Todo: This is trash. In order to properly remove a player, 
    // a) just extract them from the scene they are in and then remove
    // b) Mark them as offline, until all other players leaves scene, then remove
    internal class Unsubscribe : Event
    {
        private Id _player;
        private Player _actor;
        private bool _alreadyLoggedOut = false;

        #region Rules
        private const EventTargets _eventTargets = EventTargets.Player;
        #endregion

        public Unsubscribe(string[] parts) : this(Id.FromString(parts[0])) { } //This CTOR only converts string array to real params.

        internal Unsubscribe(Id player)
        {
            _player = player;
        }

        protected override Event Dispatch()
        {
            _actor = ResourceLocator.Get(_player) as Player;

            if(_actor == null)
            {
                _alreadyLoggedOut = true;
            }


            return this;
        }

        protected override Event Resolve()
        {
            var val = _alreadyLoggedOut ? "Already logged out" : "OK";
            var delta = new Delta
            {
                Actor = _actor,
                Key = "Logout",
                Value = val,
                Targets = ResourceLocator.GetPlayers(Result)
            };

            Result.Deltas.Add(delta);
            Result.Actor = _actor;
            Result.Targets = _eventTargets;
            Result.Resolution = EventResolutionType.Commit;

            return this;
        }

        protected override Event Persist()
        {
            if(_actor != null)
            {
                ResourceLocator.Remove(_actor);
            }

            return this;
        }
    }
}
