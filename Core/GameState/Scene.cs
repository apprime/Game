using Core.Entites.Monsters;
using Core.Entities.Humans;
using System.Collections.Generic;
using System.Linq;

namespace Core.Entities
{
    public class Scene
    {
        public Scene()
        {
            Entities = Enumerable.Empty<IEntity>();

            Location = new Location("Defaultistan", 1); //Todo: Move to GameState and keep as singleton
            Id = Id.FromString("S1234567891234567890");
            Location.Instances.Add(123, this); //Arrange Location numbers somehow? Queue?
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