using Data.Models.Entities;
using Data.Models.Entities.Humans;
using Data.Models.EventResolution;
using Data.Models.Nodes;
using Data.Repositories;
using Data.Repositories.Nodes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Core.Processes.Events
{
    internal class LeaveLocationEvent : ReadonlyEvent
    {
        private Player traveler;
        private readonly Id locationId;
        private Location location;

        private PlayerRepository personRepository;
        private LocationRepository locationRepository;

        public LeaveLocationEvent(Player traveler, Id locationId, IServiceProvider sp)
        {
            this.personRepository = sp.GetService<PlayerRepository>();
            this.locationRepository = sp.GetService<LocationRepository>();
            this.traveler = traveler;
            this.locationId = locationId;
        }

        protected override ReadonlyEvent GatherData()
        {
            var repo = new LocationRepository();
            location = locationRepository.Get(locationId);

            return this;
        }

        protected override ReadonlyEvent Resolve()
        {
            Result.Actor = traveler;
            Result.Targets = EventTargets.Nearby;
            Result.Message = string.Format("{0} has left the location", Result.Actor);
            Result.Place = location.Id;
            Result.Deltas.Add(new Delta { Actor = traveler, Key = "PlayerMovingFrom", Value = location.Name.ToString(), Targets = personRepository.Get(Result) });
            Result.Resolution = EventResolutionType.Commit;

            return this;
        }
    }
}