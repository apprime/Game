using Data.Models.Entities;
using Data.Models.Entities.Humans;
using Data.Models.Entities.Monsters;
using System.Collections.Generic;
using System.Linq;

namespace Data.Models.Gamestate
{
    public class Scene
    {
        public Scene()
        {
            Entities = Enumerable.Empty<IEntity>();

            Location = new Location("Defaultistan", 1); //Todo: Move location generation to Core(Scenebuilder).
            Id = Id.FromString("S1234567891234567890"); 
            Location.Instances.Add(123, this); //Arrange Location numbers somehow? Queue?;
        }

        public Id Id { get; set; }
        public Location Location { get; set; } 

        public IEnumerable<IEntity> Entities { get; set; }
        public List<IEntity> Players
        {
            get
            {
                return Entities.Where(i => i is Player).ToList();
            }
        }
        public List<IEntity> Enemies
        {
            get
            {
                return Entities.Where(i => i is Monster).ToList();
            }
        }
    }
}