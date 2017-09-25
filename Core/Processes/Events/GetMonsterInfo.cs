using Data.Models.Entities;
using Data.Models.Entities.Monsters;
using Data.Models.EventResolution;
using Core.ResourceManagers;
using Data.Models.Entities.Humans;
using Newtonsoft.Json;

namespace Core.Processes.Events
{
    public class GetMonsterInfo : Event
    {
        Monster _monster;
        Player _player;
        Id _monsterId;
        Id _playerId;

        EventTargets _eventTargets = EventTargets.Player;

        public GetMonsterInfo(string[] parts) : this(Id.FromString(parts[1]), Id.FromString(parts[2])) { }

        public GetMonsterInfo(Id player, Id monsterId)
        {
            _playerId = player;
            _monsterId = monsterId;
        }

        protected override Event Dispatch()
        {
            _monster = ResourceLocator.GetMonster(_monsterId.Trunk);
            _player = ResourceLocator.GetPlayer(_playerId.Trunk);
            return this;
        }

        protected override Event Persist()
        {
            //TODO: This should be a readOnly event, overloading Event Process so that you can Process without persisting
            return this;
        }

        protected override Event Resolve()
        {
            Result.Actor = _player;
            Result.Targets = _eventTargets;
            Result.Message = JsonConvert.SerializeObject(_monster);
            Result.Resolution = EventResolutionType.Commit;

            return this;
        }
    }
}
