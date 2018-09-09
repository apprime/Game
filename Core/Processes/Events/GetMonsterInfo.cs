using Data.DataProviders.Players;
using Data.Models.Entities;
using Data.Models.Entities.Humans;
using Data.Models.Entities.Monsters;
using Data.Models.EventResolution;
using Data.Repositories;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Processes.Events
{
    public class GetMonsterInfo : ReadonlyEvent
    {
        Monster _monster;
        Player _player;
        Id _monsterId;
        Id _playerId;

        EventTargets _eventTargets = EventTargets.Player;

        public GetMonsterInfo(string[] parts) : this(Id.FromString('P', parts[0]), Id.FromString('M', parts[1])) { }

        public GetMonsterInfo(Id player, Id monsterId)
        {
            _playerId = player;
            _monsterId = monsterId;
        }

        protected override async Task<ReadonlyEvent> GatherData()
        {
            var repo = new PlayerRepository(new MockedPlayerData());
            _player = await repo.Get(_playerId);

            var scene = _player.Location;

            _monster = scene.Enemies
                            .SingleOrDefault(i => i.Id == _monsterId);

            return this;
        }

        protected override ReadonlyEvent Resolve()
        {
            Result.Actor = _player;
            Result.Targets = _eventTargets;

            if (_monster == null)
            {
                Result.Message = "No monster of that type in the Scene";
                Result.Resolution = EventResolutionType.Rollback;
                return this;
            }
            else
            {
                Result.Message = JsonConvert.SerializeObject(_monster);
                Result.Resolution = EventResolutionType.Commit;
                return this;
            }
        }
    }
}
