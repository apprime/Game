using Core.Entities;
using Core.Entities.Humans;
using Core.EventResolution;
using Core.ResourceManagers;
using System.Collections.Generic;

namespace Core.Events
{
    public abstract class Event
    {
        protected EventResult Result = new EventResult();

        public delegate void EventHandler(EventResult result);
        public static event EventHandler EventResolved;

        internal Event Process()
        {
            this.Dispatch()
                .Resolve()
                .Persist()
                .Broadcast();

            return this;
        }

        /// <summary>
        /// Fetch or create the needed resources
        /// </summary>
        /// <returns></returns>
        protected abstract Event Dispatch();

        /// <summary>
        /// Create an eventResolution object for broadcasting
        /// </summary>
        /// <returns></returns>
        protected abstract Event Resolve();

        /// <summary>
        /// Persist the change set in memory
        /// (i.e. make the changes)
        /// TODO: There is a major todo lurking here. 
        /// We need some way of locking resources and 
        /// rollbacking if they cant be committed.
        /// </summary>
        /// <returns></returns>
        protected abstract Event Persist();

        /// <summary>
        /// Broadcast the change set to all relevant targets.
        /// </summary>
        internal virtual void Broadcast()
        {
            EventResolved?.Invoke(Result);
        }
    }
}