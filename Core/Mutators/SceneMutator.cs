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

            var origin = movement.Origin;
            origin.RemovePlayer(movement.Traveler);

            //Dispose the Scene if noone is there.
            if (!origin.Players.Any())
            {
                origin.Location.RemoveScene(movement.Origin);
            }
        }

        internal static Movement SetOrigin(Movement movement)
        {
            var actor = movement.Traveler;

            //If Player is "nowhere", they either just logged in or glitched out. Either way, return to sender.
            if(actor.Scene == null)
            {
                movement.Origin = SceneFactory.GetOrCreate(actor.LoggedOutPosition, actor);
            }
            else
            {
                movement.Origin = actor.Scene;
            }

            return movement;
        }

        internal static Movement SetDestination(Position position, Movement movement)
        {
            var newScene = SceneFactory.GetOrCreate(position, movement.Traveler);
            movement.Destination = newScene;

            return movement;
        }

        internal static Movement SetActor(Player actor, Movement movement)
        {
            movement.Traveler = actor;
            return movement;
        }
    }
}
