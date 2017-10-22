﻿using Data.Models.Entities;
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
        public Scene(Location location)
        {
            Entities = new List<IEntity>();
            Location = location;
            Id = Id.FromParts('S', Location.Position);
        }

        public Id Id { get; set; }
        public Location Location { get; set; } 
        public Position Position { get { return Location.Position; } }

        public List<IEntity> Entities { get; set; }
        public IEnumerable<Player> Players
        {
            get
            {
                return Entities.OfType<Player>();
            }
        }
        public IEnumerable<Monster> Enemies
        {
            get
            {
                return Entities.OfType<Monster>();
            }
        }

        public void RemovePlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public void AddPlayer(Player player)
        {
            Entities.Add(player);
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