using Data.Models.Entities;
using Data.Models.Entities.Humans;
using Data.Models.EventResolution;
using Data.Models.Nodes;
using Data.Repositories;
using Data.Repositories.Nodes;

namespace Core.Processes.Events
{
    internal class LeaveLocationEvent : ReadonlyEvent
    {
        private Player traveler;
        private Id locationId;
        private Location location;

        public LeaveLocationEvent(Player traveler, Id locationId)
        {
            this.traveler = traveler;
            this.locationId = locationId;
        }

        protected override ReadonlyEvent GatherData()
        {
            var repo = new LocationRepository();
            location = repo.Get(locationId);

            return this;
        }

        protected override ReadonlyEvent Resolve()
        {
            var repo = new PlayerRepository();

            Result.Actor = traveler;
            Result.Targets = EventTargets.Nearby;
            Result.Message = string.Format("{0} has left the location", Result.Actor);
            Result.Place = location.InstanceId;
            Result.Deltas.Add(new Delta { Actor = traveler, Key = "PlayerMovingFrom", Value = location.Name.ToString(), Targets = repo.Get(Result) });
            Result.Resolution = EventResolutionType.Commit;

            return this;
        }
    }
}