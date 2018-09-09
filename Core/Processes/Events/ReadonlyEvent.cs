using Data.Models.EventResolution;
using System.Threading.Tasks;

namespace Core.Processes.Events
{
    public abstract class ReadonlyEvent
    {
        protected EventResult Result = new EventResult();

        public delegate void EventHandler(EventResult result);
        public static event EventHandler EventResolved;

        public virtual async Task<ReadonlyEvent> Process()
        {
            await GatherData();
            Resolve();
            Broadcast();

            return this;
        }

        /// <summary>
        /// Fetch or create the needed resources
        /// </summary>
        /// <returns></returns>
        protected abstract Task<ReadonlyEvent> GatherData();

        /// <summary>
        /// Create an eventResolution object for broadcasting
        /// </summary>
        /// <returns></returns>
        protected abstract ReadonlyEvent Resolve();

        /// <summary>
        /// Broadcast the change set to all relevant targets.
        /// </summary>
        protected virtual void Broadcast()
        {
            EventResolved?.Invoke(Result);
        }
    }
}