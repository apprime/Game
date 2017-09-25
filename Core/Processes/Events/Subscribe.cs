using Core.Factories;
using Core.ResourceManagers;
using Data.Models.Entities;
using Data.Models.Entities.Humans;
using Data.Models.Entities.Monsters;
using Data.Models.Entities.EntityInterfaces;
using Data.Models.EventResolution;
using Data.Models.Gamestate;

namespace Core.Processes.Events
{
    public class Subscribe : Event
    {
        private Id _id;
        private Player _player;
        private string _connectionId;
        private bool _alreadyLoggedIn = false;

        #region Rules
        private const EventTargets _eventTargets = EventTargets.Player;
        #endregion

        public Subscribe(string[] parts) : this(Id.FromString(parts[1]), parts[2]) { } //This CTOR only converts string array to real params.

        internal Subscribe(Id player, string connectionId)
        {
            _id = player;
            _connectionId = connectionId;
        }

        protected override Event Dispatch()
        {
            _player = ResourceLocator.Get(_id) as Player;

            if (_player == null)
            {
                _player = (new Data.Repositories.PlayerRepository()).Get(_id, _connectionId);
            }
            else
            {
                _alreadyLoggedIn = true;
            }

            return this;
        }

        protected override Event Resolve()
        {
            var val = _alreadyLoggedIn ? "Already logged in" : "OK";
            Result.Deltas.Add(new Delta { Actor = _player, Key = "Login", Value = val, Targets = ResourceLocator.GetPlayers(Result) });
            Result.Actor = _player;
            Result.Targets = _eventTargets;
            Result.Resolution = EventResolutionType.Commit;

            return this;
        }

        protected override Event Persist()
        {
            ResourceLocator.Add(_player);

            //TEMPORARY WORKAROUND TO ALWAYS SPAWN A MONSTER WHEN PLAYER SUBSCRIBES:
            var todoScene = (Scene)ResourceLocator.Get(Id.FromString("S123"));
            if(todoScene == null)
            {
                todoScene = new Scene();
                ResourceLocator.Add(todoScene);
            }

            var monster = (Monster)ResourceLocator.Get(Id.FromString("M123"));
            if (monster == null)
            {
                monster = MonsterFactory.Create("A");
                ResourceLocator.Add(monster);
                todoScene.Entities.Add(monster);
            }

            //TODO: Look at this! We should be able to have one way references.
            todoScene.Entities.Add(_player);
            _player.Scene = todoScene;

            return this;
        }
    }
}
