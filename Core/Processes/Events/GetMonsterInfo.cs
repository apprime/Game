using Data.Models.Entities;
using Data.Models.Entities.Monsters;
using Data.Models.EventResolution;
using Core.ResourceManagers;
using Data.Models.Entities.Humans;
using Newtonsoft.Json;
using Data.Models.Exceptions;
using System.Linq;

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

        public GetMonsterInfo(string[] parts) : this(Id.FromString(parts[0]), Id.FromString(parts[1])) { }

        public GetMonsterInfo(Id player, Id monsterId)
        {
            _playerId = player;
            _monsterId = monsterId;
        }

        protected override ReadonlyEvent GatherData()
        {
            _monster = ResourceLocator.GetMonster(_monsterId.Trunk);
            _player = ResourceLocator.GetPlayer(_playerId.Trunk);
            return this;
        }

        protected override ReadonlyEvent Resolve()
        {
            if(!playerMayViewMonster)
            {
                throw new TodoException("Handle error thrown by player attempting illegal action");
            }

            Result.Actor = _player;
            Result.Targets = _eventTargets;
            Result.Message = JsonConvert.SerializeObject(_monster);
            Result.Resolution = EventResolutionType.Commit;

            return this;
        }
    }
}
