using Data.Factories;
using Data.Models.Entities;
using Data.Models.Entities.Humans;
using Data.Models.Nodes;
using System.Linq;

namespace Core.Mutators
{
    public static class LocationMutator
    {
        public static void GoToPosition(Movement movement)
        {
            movement.Destination.AddPlayer(movement.Traveler);
            movement.Traveler.Location = movement.Destination;
        }

        internal static void Cleanup(Movement movement)
        {
            var origin = movement.Origin;
            origin.RemovePlayer(movement.Traveler);

            //Dispose the Scene if noone is there.
            if (!origin.Players.Any())
            {
                origin.Parent.RemoveLocation(movement.Origin);
            }
        }

        internal static void SetOrigin(Movement movement)
        {
            movement.Origin = movement.Traveler.Location;
        }

        internal static void SetDestination(Position position, Movement movement)
        {
            var newLocation = LocationFactory.GetOrCreate(position, movement.Traveler);
            movement.Destination = newLocation;
        }

        internal static void SetTraveler(Player actor, Movement movement)
        {
            movement.Traveler = actor;
        }
    }
}
