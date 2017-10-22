using Data.Models.Entities;
using Data.Models.Entities.Monsters;
using Data.Models.EventResolution;
using Core.ResourceManagers;
using Data.Models.Entities.Humans;
using Newtonsoft.Json;
using Data.Models.Exceptions;
using System.Linq;
using Data.DataProviders.Players;
using Data.Repositories;

namespace Core.Processes.Events
{
    public class GetMonsterInfo : ReadonlyEvent
    {
        Monster _monster;
        Player _player;
        Id _monsterId;
        Id _playerId;

        EventTargets _eventTargets = EventTargets.Player;
        private bool playerMayViewMonster => _player.Scene.Enemies.Any(i => i.Id.Equals(_monsterId));

        public GetMonsterInfo(string[] parts) : this(Id.FromString(parts[0]), Id.FromString('M', parts[1])) { }

        public GetMonsterInfo(Id player, Id monsterId)
        {
            _playerId = player;
            _monsterId = monsterId;
        }

        protected override ReadonlyEvent GatherData()
        {
            var repo = new PlayerRepository(new MockedPlayerData());
            _player = repo.Get(_playerId);

            var scene = _player.Scene;

            _monster = scene.Enemies
                            .SingleOrDefault(i => i.Id == _monsterId);

            return this;
        }

        protected override ReadonlyEvent Resolve()
        {
            if(_monster == null)
            {
                Result.Message = "No monster of that type in the Scene";
                Result.Resolution = EventResolutionType.Rollback;
                return this;
            }

            Result.Actor = _player;
            Result.Targets = _eventTargets;
            Result.Message = JsonConvert.SerializeObject(_monster);
            Result.Resolution = EventResolutionType.Commit;

            return this;
        }
    }
}
