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

namespace Core
{
    public class Engine
    {
        private BatchQueue<Event> EventList { get; set; }

        public bool run = false;
        public int TickRate = 1000;
        private int batchSize = 1;
        private int threadSize = 1;

        public static Engine Instance{ get; }

        static Engine()
        {
            Instance = new Engine();
        }

        private Engine()
        {
            EventList = new BatchQueue<Event>();
        }

        //Todo: Loop has two problems. 
        //1) It cannot return a result because it runs on a different thread 
        //2) It doesnt seem to properly create a thread 
        public void Loop()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while(run)
            {
                //Todo: Always tick for at most TickRate time.
                //Todo: Above means threading for more tick actions.
                //Todo: Long running operations?
                if (sw.ElapsedMilliseconds >= TickRate) //OR MaxQueueSize is reached? To prevent backlog.
                {
                    //Note: This might leave some events for next tick, but that is fine since it's a queue
                    var chunkSize = EventList.Count / threadSize; 
                    Tick(threadSize, chunkSize);
                    sw.Restart();
                }
            }
        }

        private void Tick(int threadCount, int chunkSize)
        {
            for(var i = 0; i<threadCount; i++)
            {
                var eventChunk = EventList.DequeueChunk(chunkSize);
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
            EventList.Enqueue(e);
        }

        //Todo: string -> IEvent as attribute?
        public void Push(string eventString)
        {
            Push(EventResolver.Resolve(eventString));
        }

        private IEnumerable<Event> Tick()
        {
            Debug.WriteLine("Ticking");
            for(var i = 0; i > batchSize; i++)
            {
                yield return EventList.Dequeue().Process();
            }
        }

        //Todo: This is probably not needed, but nice to have for dev purposes
        public void StartEngine()
        {
            Debug.WriteLine("Starting Engine");
            run = true;
            Loop(); //This doesnt work and you know it. We need a good way to start the engine and then feed it
        }

        //Todo: This is probably not needed, but nice to have for dev purposes
        public void StopEngine()
        {
            Debug.WriteLine("Stopping Engine");
            run = false;
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
