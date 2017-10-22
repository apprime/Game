using Data.Factories;
using Data.Models.Entities;
using Data.Models.Gamestate;
using System.Collections.Generic;

namespace Data.Models.Nodes
{
    public class GlobalLocation : Location
    {
        private Scene[] _scenes = new Scene[1];

        public GlobalLocation(string name, Position position, IEnumerable<Seed> seeds)
            : base(name, position)
        {
            Seeds = seeds;
            _scenes[0] = SceneFactory.Create(this);
        }

        public override IList<Scene> Scenes
        {
            get
            {
                return _scenes;
            }
        }
    }
}
