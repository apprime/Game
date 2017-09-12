﻿using Core.Entities;
using Core.EventResolution;
using System;

namespace Core.Events
{
    internal class KillEvent : Event
    {
        public Id Target { get; set; }
        public Id Actor { get; set; }

        private EventTargets _validTargets = EventTargets.Nearby;

        public KillEvent(Id entityId, Id actorId)
        {

        }

        protected override Event Dispatch()
        {
            throw new NotImplementedException();
        }

        protected override Event Resolve()
        {
            throw new NotImplementedException();
        }

        protected override Event Persist()
        {
            throw new NotImplementedException();
        }
    }
}