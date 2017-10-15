using Data.Models.Entities;
using Data.Models.Entities.EntityInterfaces;
using Data.Models.Entities.Humans;
using Data.Models.Entities.Monsters;
using Data.Models.Nodes;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Data.Models.Gamestate
{
    public class Scene : IEntity
    {
        public Scene()
        {
            Entities = new List<IEntity>();
            //TODO: S + Position + Trunk
            Id = Id.FromString("S1111123"); 
        }

        public Scene(Location location)
        {
            Location = location;
        }

        public Id Id { get; set; }
        public Location Location { get; set; } 
        public Position Position { get { return Location.Position; } }

        public List<IEntity> Entities { get; set; }
        public IEnumerable<IEntity> Players
        {
            get
            {
                return Entities.Where(i => i is Player);
            }
        }
        public IEnumerable<IEntity> Enemies
        {
            get
            {
                return Entities.Where(i => i is Monster);
            }
        }

        public void RemovePlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public void AddPlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public string Name
        {
            get
            {
                return Location.Name;
            }
        }
    }
}