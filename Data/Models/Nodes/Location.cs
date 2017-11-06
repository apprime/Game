using Data.Models.Entities;
using Data.Models.Entities.EntityInterfaces;
using Data.Models.Entities.Humans;
using Data.Models.Entities.Monsters;
using Data.Repositories.Nodes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Data.Models.Nodes
{
    /// <summary>
    /// There are two types of locations in the world. One the is a singleton, and one that might be instantiated.
    /// A singleton location will simply contain every player that tries to visit it
    /// A instanced location will generate new Scenes for each player/group that visits it.
    /// </summary>
    public class Location : IEntity
    {
        public Sector Sector { get; } //TODO: Make sure this is properly set on create
        public List<Seed> Seeds;
        public Id Id { get; }

        private string todoTrunk = "123";
        
        [JsonConstructor]
        public Location(string name, Position position)
        {
            Name = name;
            Position = position;
            Seeds = new List<Seed>();
            var sectorRepo = new SectorRepository();
            Sector = sectorRepo.Get(position);
            Sector.Locations.Add(this);

            Entities = new List<IEntity>();
            Id = Id.FromParts('L', position, todoTrunk);
        }

        public string Name { get; set; }
        public Position Position { get; set; }
        public string ImageUrl { get; set; }

        public IList<Position> Neighbours { get; set;  } = new List<Position>(); //TODO: This should be set upon load and be enumerable, not list

        public bool HasNeighbour(Position p)
        {
            return Neighbours.Any(i => i == p);
        }

        [JsonIgnore]
        public List<IEntity> Entities { get; set; }

        [JsonIgnore]
        public IEnumerable<Player> Players
        {
            get
            {
                return Entities.OfType<Player>();
            }
        }

        [JsonIgnore]
        public IEnumerable<Monster> Enemies
        {
            get
            {
                return Entities.OfType<Monster>();
            }
        }

        public void RemovePlayer(Player player)
        {
            Entities.Remove(player);
            if(!Players.Any())
            {
                this.Sector.RemoveLocation(this);
            }
        }

        public void AddPlayer(Player player)
        {
            Entities.Add(player);
        }
    }
}
