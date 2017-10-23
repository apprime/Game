using Data.Factories;
using Data.Models.Entities;
using System.Collections.Generic;

namespace Data.Models.Nodes
{
    public class GlobalLocation : Location
    {
        public GlobalLocation(string name, Position position, List<Seed> seeds)
            : base(name, position)
        {
            Seeds = seeds;
            Scenes.Add(SceneFactory.Create(this));
        }
    }
}
