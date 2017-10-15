using Data.Models.Entities.Humans;
using Data.Models.Gamestate;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Models.Nodes
{
    /// <summary>
    /// There are two types of locations in the world. One the is a singleton, and one that might be instantiated.
    /// A singleton location will simply contain every player that tries to visit it
    /// A instanced location will generate new Scenes for each player/group that visits it.
    /// </summary>
    public class Location : ILocation
    {
        public Region Region { get; } //TODO: Make sure this is properly set on create
        
        [JsonConstructor]
        public Location(string name, Position position)
        {
            Name = name;
            Position = position;
            Scenes = new List<Scene>();
        }

        public string Name { get; set; }
        public Position Position { get; set; }

        public virtual IList<Scene> Scenes { get; private set; }

        public IEnumerable<Player> GetPlayers(int instanceId)
        {
            //Sometimes we might want to cross over multiple Scenes in one Location, or maybe we only want this for singleton Locations?
            throw new NotImplementedException();
        }

        public void RemoveScene(Scene previousScene)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Location> Neighbours { get; } = Enumerable.Empty<Location>(); //TODO: This should be set upon load!

        public bool HasNeighbour(Position p)
        {
            return Neighbours.Any(i => i.Position.Equals(p));
        }
    }
}
