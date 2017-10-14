using Data.Models.Entities;
using Data.Models.Gamestate;
using Data.Models.Nodes;
using Newtonsoft;
using Newtonsoft.Json.Linq;

namespace Core.Processes.Generator
{
    public class SceneBuilder
    {
        private Scene _scene;

        public SceneBuilder()
        {
            Seed();
        }

        public SceneBuilder(string preset)
        {
            //TODO: Scene To/From json
            Scene data = JObject.Parse(preset).ToObject<Scene>();


            _scene.Location = data.Location;
            //etc.
        }

        public SceneBuilder SetTheme(Theme theme)
        {
            //Adds graphical theme. 
            //Background 
            //Sets restrictions on types for: Monsters,Loot, NPCs, Quests, Vendors
            // TODO : Blah
            return this;
        }

        public SceneBuilder SetLevel(Level level)
        {
            //Sets a level for the scene. This should take into account:
            //Player level, party size, Theme?, more?
            return this;
        }

        public Scene Build()
        {
            return this._scene;
        }

        private void Seed()
        {
            //Generate a one-of random seed to use for the entire build process.
        }

        //TODO: This should be a static shared method for all entities. Where?
        private Id GenerateId()
        {
            return Id.Create('S');
        }

        //TODO: This represents string fetched from somewhere
        private JObject test = new JObject
        {
            { "Location", "value" },
            {
                "list", new JArray
                {
                    "a", "b", "c"
                }
            }
        };
    }
}
