using System.Diagnostics;
using Core.EventResolution;
using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Core.ResourceManagers;
using Core.Factories;
using Core.Events;
using Core.Entities.Humans;
using System.Threading;
using System;

namespace Core
{
    public class Engine
    {
        private BatchQueue<Event> _eventList;

        //TODO: We want to be able to set these values in config I suppose?
        public bool run = false;
        private int _tickRate = 1000;
        private int _threadNumber = 1;
        private int MaxQueueSize = 100;
        public AutoResetEvent _loopTimer;
        private Timer _stateTimer;

        public static Engine Instance{ get; }

        static Engine()
        {
            Instance = new Engine();
        }

        private Engine()
        {
            _eventList = new BatchQueue<Event>();
            _loopTimer = new AutoResetEvent(false);
        }

        private void Tick()
        {
            var chunkSize = _eventList.Count / _threadNumber;

            for (var i = 0; i < _threadNumber; i++)
            {
                var eventChunk = _eventList.DequeueChunk(chunkSize);
                var t = Task.Factory.StartNew(
                    () => 
                    {
                        ProcessBatch(eventChunk);
                    });
                t.Start();
            }
        }

        private void ProcessBatch(IEnumerable<Event> batch)
        {
            foreach(var item in batch)
            {
                item.Process();
            }
        }

        public void Push(Event e)
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

        //Todo: This is probably not needed, but nice to have for dev purposes
        public void StartEngine()
        {
            Debug.WriteLine("Starting Engine");
            _stateTimer = new Timer((a) => Tick(), _loopTimer, _tickRate, _tickRate);
        }

        //Todo: This is probably not needed, but nice to have for dev purposes
        public void StopEngine()
        {
            Debug.WriteLine("Stopping Engine");
            _stateTimer.Dispose();
        }

        public void Subscribe(Player p)
        {
            //TODO: Crashing on duplicates
            Debug.WriteLine("Subscribing " +p.Name);
            ResourceLocator.Add(p);

            //TEMPORARY WORKAROUND TO ALWAYS SPAWN A MONSTER WHEN PLAYER SUBSCRIBES:
            var monster = MonsterFactory.Create("A");
            ResourceLocator.Add(monster);

            var todoScene = new Scene();
            todoScene.Enemies.Add(monster);
            todoScene.Players.Add(p);

            p.Scene = todoScene;
        }

        //Todo: This is trash. In order to properly remove a player, 
        // a) just extract them from the scene they are in and then remove
        // b) Mark them as offline, until all other players leaves scene, then remove
        public void Unsubscribe(Player p)
        {
            //TODO: Crashes on key not found
            Debug.WriteLine("Unsubscribing " +p.Name);
            ResourceLocator.Remove(p);
        }
    }
}
