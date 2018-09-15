using Data.DataProviders.Players;
using Data.Models.Entities;
using Data.Models.Entities.Humans;
using Data.Models.EventResolution;
using Data.Models.Nodes;
using Data.Repositories;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Processes.Events
{
    public class GetMyLocationEvent : ReadonlyEvent
    {
        Location _location;
        Player _player;
        Id _playerId;

        EventTargets _eventTargets = EventTargets.Player;
        private readonly PlayerRepository playerRepository;

        public GetMyLocationEvent(string[] parts, IServiceProvider sp) : this(Id.FromString(parts[0]))
        {
            playerRepository = sp.GetService<PlayerRepository>();
        }

        public GetMyLocationEvent(Id player)
        {
            _playerId = player;
        }

        protected override ReadonlyEvent GatherData()
        {
            _player = playerRepository.Get(_playerId);
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
