using Data.Models.Entities;
using Data.Models.EventResolution;
using System;

namespace Core.Processes.Events
{
    internal class KillEvent : Event
    {
        public Id Target { get; set; }
        public Id Actor { get; set; }

        private EventTargets _validTargets = EventTargets.Nearby;

        public KillEvent(Id entityId, Id actorId)
        {

        }

        protected override ReadonlyEvent GatherData()
        {
            throw new NotImplementedException();
        }

        protected override ReadonlyEvent Resolve()
        {
            throw new NotImplementedException();
        }

        protected override Event Persist()
        {
            throw new NotImplementedException();
        }
    }
}
