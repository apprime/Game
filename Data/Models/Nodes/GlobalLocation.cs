using Data.Models.Gamestate;
using Data.Repositories.Nodes;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Data.Models.Nodes
{
    public class GlobalLocation : Location
    {
        private ImmutableArray<Scene> scenes;

        public GlobalLocation(string name, Position position) : base(name, position)
        {
            var globalScene = (new SceneRepository()).Create(this);
            scenes = ImmutableArray.Create<Scene>().Add(globalScene);
        }

        public override IList<Scene> Scenes
        {
            get
            {
                return scenes;
            }
        }
    }
}
