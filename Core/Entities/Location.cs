using Core.Entities.Humans;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Location : ILocation
    {
        public static Dictionary<int, Scene> Instances { get; set; }

        static Location()
        {
            Instances = new Dictionary<int, Scene>();
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
            throw new NotImplementedException();
        }
    }
}
