using Data.DataProviders.Players;
using Data.Models.Entities;
using Data.Models.Entities.Humans;
using Data.Models.EventResolution;
using Data.Models.Nodes;
using Data.Repositories;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Core.Processes.Events
{
    public class GetMyLocationEvent : ReadonlyEvent
    {
        Location _location;
        Player _player;
        Id _playerId;

        EventTargets _eventTargets = EventTargets.Player;

        public GetMyLocationEvent(string[] parts) : this(Id.FromString(parts[0])) { }

        public GetMyLocationEvent(Id player)
        {
            _playerId = player;
        }

        protected override async Task<ReadonlyEvent> GatherData()
        {
            var repo = new PlayerRepository();
            _player = await repo.Get(_playerId);
            _location = _player.Location;
            
            return this;
        }

        protected override ReadonlyEvent Resolve()
        {
            Result.Actor = _player;
            Result.Targets = _eventTargets;
            Result.Message = JsonConvert.SerializeObject(_location);
            Result.Resolution = EventResolutionType.Commit;

            return this;
        }
    }
}
