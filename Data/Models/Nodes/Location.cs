using Data.Models.Entities.Humans;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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

        static Location()
        {
        }

        [JsonConstructor]
        public Location(string name, int position)
        {
            Name = name;
            Position = position;
        }

        public string Name { get; set; }
        public int Position { get; set; } //TODO: change to pos struct

        public IEnumerable<Player> GetPlayers(int instanceId)
        {
            //Sometimes we might want to cross over multiple Scenes in one Location, or maybe we only want this for singleton Locations?
            throw new NotImplementedException();
        }
    }
}
