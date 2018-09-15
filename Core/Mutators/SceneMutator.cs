using Data.Factories;
using Data.Models.Entities;
using Data.Models.Entities.Humans;
using Data.Models.Nodes;

namespace Core.Mutators
{
    public static class LocationMutator
    {
        public static void GoToPosition(Movement movement)
        {
            movement.Destination.AddPlayer(movement.Traveler);
            movement.Traveler.Location = movement.Destination;
        }

        internal static void RemovePlayerFromScene(Movement movement)
        {
            movement.Origin.RemovePlayer(movement.Traveler);
        }

        internal static void RemoveEmptyScene(Movement movement)
        {
            movement.Origin.Sector.RemoveLocation(movement.Origin);
        }

        internal static void SetOrigin(Movement movement)
        {
            movement.Origin = movement.Traveler.Location;
        }

        internal static void SetDestination(Position position, Movement movement)
        {
            var newLocation = LocationFactory.GetOrCreate(position, movement.Traveler).Result;
            movement.Destination = newLocation;
        }

        internal static void SetTraveler(Player actor, Movement movement)
        {
            movement.Traveler = actor;
        }
    }
}
