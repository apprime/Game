using Core.ResourceManagers;
using Data.DataProviders.Players;
using Data.Models.Entities;
using Data.Models.Entities.Humans;
using Data.Models.EventResolution;
using Data.Repositories;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Processes.Events
{
    public class SubscribeEvent : Event
    {
        private Id _id;
        private Player _player;
        private string _connectionId;
        private bool _alreadyLoggedIn = false;
        private readonly PlayerRepository playerRepository;

        #region Rules
        private const EventTargets _eventTargets = EventTargets.Player;
        #endregion

        public SubscribeEvent(string[] parts, IServiceProvider sp) : this(Id.FromString('P', parts[0]), parts[1])
        {
            playerRepository = sp.GetService<PlayerRepository>();
        } //This CTOR only converts string array to real params.

        internal SubscribeEvent(Id player, string connectionId)
        {
            _id = player;
            _connectionId = connectionId;
        }

        protected override ReadonlyEvent GatherData()
        {
            _player = playerRepository.Get(_id) as Player;

            if (_player == null)
            {
                _player = playerRepository.Load(_id, _connectionId);
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
                Result.Deltas.Add(new Delta { Actor = _player, Key = "Login", Value = "OK", Targets = playerRepository.Get(Result) });
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
                playerRepository.Add(_player);

                var dumpPlayerAt = new EnterLocationEvent(_id, _player.LoggedOutPosition);
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
