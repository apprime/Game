using Data.Models.Gamestate;

namespace Core.Processes.ContentGeneration
{
    public class SceneBuilder
    {
        private Scene _scene;

        public SceneBuilder()
        {
            Seed();
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
    }
}
