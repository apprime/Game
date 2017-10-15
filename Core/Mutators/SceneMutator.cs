using Core.Processes.ContentGeneration;
using Data.Models.Entities.Humans;
using Data.Models.Nodes;
using System.Linq;
using System;
using Data.Models.Entities;

namespace Core.Mutators
{
    public static class SceneMutator
    {
        public static void GoToPosition(Movement movement)
        {
            movement.Destination.AddPlayer(movement.Traveler);
            movement.Traveler.Scene = movement.Destination;
        }

        internal static void Cleanup(Movement movement)
        {
            var origin = movement.Origin;
            origin.RemovePlayer(movement.Traveler);

            //Dispose the Scene if noone is there.
            if (!origin.Players.Any())
            {
                origin.Location.RemoveScene(movement.Origin);
            }
        }

        internal static void SetOrigin(Movement movement)
        {
            movement.Origin = movement.Traveler.Scene;
        }

        internal static void SetDestination(Position position, Movement movement)
        {
            var newScene = SceneFactory.GetOrCreate(position, movement.Traveler);
            movement.Destination = newScene;
        }

        internal static void SetTraveler(Player actor, Movement movement)
        {
            movement.Traveler = actor;
        }
    }
}
