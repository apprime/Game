using Core.Processes.Events;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    public class Engine
    {
        private BatchQueue<ReadonlyEvent> _eventList;

        //TODO: We want to be able to set these values in config I suppose?
        public bool run = false;
        private int _tickRate = 1000;
        private int _threadNumber = 1;
        private int MaxQueueSize = 1;
        public AutoResetEvent _loopTimer;
        private Timer _stateTimer;

        public static Engine Instance{ get; }

        static Engine()
        {
            Instance = new Engine();
        }

        private Engine()
        {
            _eventList = new BatchQueue<ReadonlyEvent>();
            _loopTimer = new AutoResetEvent(false);
        }

        private void Tick()
        {
            var chunkSize = _eventList.Count / _threadNumber;

            for (var i = 0; i < _threadNumber; i++)
            {
                var eventChunk = _eventList.DequeueChunk(chunkSize);
                Task.Factory.StartNew(() => ProcessBatch(eventChunk));
            }
        }

        private void ProcessBatch(IEnumerable<ReadonlyEvent> batch)
        {
            foreach(var item in batch)
            {
                item.Process();
            }
        }

        public void Push(ReadonlyEvent e)
        {
            _eventList.Enqueue(e);
            //We could use an event to trigger if the queue grows too big, but since we know when we are adding to it, I don't think we need it.
            //TODO:  monitoring hook that can alert when overflow is beginnning
            //TODO:  autoscaling of potential cloud server (I know, super important in early access super pre-alpha)
            PreventOverflow();
        }

        private void PreventOverflow()
        {
            if(_eventList.Count >= MaxQueueSize)
            {
                Tick();
                _loopTimer.Reset(); //Don't double dip with events.
            }
        }
    }
}
