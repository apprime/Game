using Core.ResourceManagers;
using Data.DataProviders.Players;
using Data.Models.Entities;
using Data.Models.Entities.Humans;
using Data.Models.EventResolution;
using Data.Repositories;

namespace Core.Processes.Events
{
    public class SubscribeEvent : Event
    {
        private Id _id;
        private Player _player;
        private string _connectionId;
        private bool _alreadyLoggedIn = false;

        #region Rules
        private const EventTargets _eventTargets = EventTargets.Player;
        #endregion

        public SubscribeEvent(string[] parts) : this(Id.FromString('P', parts[0]), parts[1]) { } //This CTOR only converts string array to real params.

        internal SubscribeEvent(Id player, string connectionId)
        {
            _id = player;
            _connectionId = connectionId;
        }

        protected override ReadonlyEvent GatherData()
        {
            var repo = new PlayerRepository(new MockedPlayerData());
            _player = repo.Get(_id) as Player;

            if (_player == null)
            {
                _player = repo.Load(_id, _connectionId);
            }
            else
            {
                _alreadyLoggedIn = true;
            }

            return this;
        }

        protected override ReadonlyEvent Resolve()
        {
            if(_alreadyLoggedIn)
            {
                Result.Message = "Already logged in";
                Result.Resolution = EventResolutionType.Rollback;
            }
            else
            {
                Result.Message = "You logged in";
                Result.Deltas.Add(new Delta { Actor = _player, Key = "Login", Value = "OK", Targets = ResourceLocator.GetPlayers(Result) });
                Result.Resolution = EventResolutionType.Commit;
            }

            Result.Actor = _player;
            Result.Targets = _eventTargets;

            return this;
        }

        protected override Event Persist()
        {
            if(Result.Resolution == EventResolutionType.Commit)
            {
                var repo = new PlayerRepository(new MockedPlayerData());
                repo.Add(_player);

                var dumpPlayerAt = new ChangeLocationEvent(_id, _player.LoggedOutPosition);
                Engine.Instance.Push(dumpPlayerAt);
            }

            // TEMPORARY WORKAROUND TO ALWAYS SPAWN A MONSTER WHEN PLAYER SUBSCRIBES:
            //var monster = (Monster)ResourceLocator.Get(Id.FromString("M1111123"));
            //if (monster == null)
            //{
            //    monster = MonsterFactory.Create("A");
            //    ResourceLocator.Add(monster);
            //    _player.Scene.Entities.Add(monster);
            //}

            //TODO: Look at this! We should be able to have one way references.
            //_player.Scene.Entities.Add(_player);
            //_player.Scene = todoScene;

            return this;
        }
    }
}
