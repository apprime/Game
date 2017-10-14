using Data.Models.Entities;
using Data.Models.Entities.EntityInterfaces;
using Data.Models.Entities.Humans;
using Data.Models.Entities.Monsters;
using Data.Models.Nodes;
using System.Collections.Generic;
using System.Linq;

namespace Data.Models.Gamestate
{
    public class Scene : IEntity
    {
        public Scene()
        {
            Entities = new List<IEntity>();

            //Location = new Location("Defaultistan", 1); //Todo: Move location generation to Core(Scenebuilder).
            Id = Id.FromString("S123"); 
            //Location.Instances.Add(123, this); //Arrange Location numbers somehow? Queue?;
        }

        public Id Id { get; set; }
        public Location Location { get; set; } 

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

        public string Name
        {
            get
            {
                return Location.Name;
            }
        }
    }
}