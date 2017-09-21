using Data.Models.Entities;
using Data.Models.Entities.Humans;
using Data.Models.EventResolution;
using Core.Mutators;
using Core.ResourceManagers;
using System;

namespace Core.Events
{
    internal class Unsubscribe : Event
    {
        private Id _player;
        private Player _actor;

        #region Rules
        private const EventTargets _eventTargets = EventTargets.Player;
        #endregion

        public Unsubscribe(string[] parts) : this(Id.FromString(parts[1])) { } //This CTOR only converts string array to real params.

        internal Unsubscribe(Id player)
        {
            _player = player;
        }

        protected override Event Dispatch()
        {
            return this;
        }

        protected override Event Resolve()
        {
            Result.Deltas.Add(new Delta { Actor = _actor, Key = "Login", Value = "OK", Targets = ResourceLocator.GetPlayers(Result) });
            Result.Actor = _actor;
            Result.Targets = _eventTargets;
            Result.Resolution = EventResolutionType.Commit;

            return this;
        }

        protected override Event Persist()
        {
            ResourceLocator.Remove(_actor);

            return this;
        }
    }
}
