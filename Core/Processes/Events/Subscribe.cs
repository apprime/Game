using Core.ResourceManagers;
using Data.Models.Entities;
using Data.Models.Entities.Humans;
using Data.Models.EventResolution;

namespace Core.Events
{
    internal class Subscribe : Event
    {
        private Id _id;
        private Player _player;

        #region Rules
        private const EventTargets _eventTargets = EventTargets.Player;
        #endregion

        public Subscribe(string[] parts) : this(Id.FromString(parts[1])) { } //This CTOR only converts string array to real params.

        internal Subscribe(Id player)
        {
            _id = player;
        }

        protected override Event Dispatch()
        {

            _player = ResourceLocator.Get(_id) as Player;

            return this;
        }

        protected override Event Resolve()
        {
            Result.Deltas.Add(new Delta { Actor = _player, Key = "Login", Value = "OK", Targets = ResourceLocator.GetPlayers(Result) });
            Result.Actor = _player;
            Result.Targets = _eventTargets;
            Result.Resolution = EventResolutionType.Commit;

            return this;
        }

        protected override Event Persist()
        {
            ResourceLocator.Add(_player);

            return this;
        }
    }
}
