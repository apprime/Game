using Data.Models.Gamestate;

namespace Data.Models.Entities
{
    //TODO: Deal with the heirarchy of objects.
    //Preferably, game world should be highest, non targetable.
    //Then comes Actors and Targets. Actors can act, targets can be altered
    //Players and game monsters are both. 
    //Game only acts. 
    //objects such as vendors, chests etc, only reacts. 
    public interface IEntity
    {
        /// <summary>
        /// Todo: Evaluate if object is too bulky
        /// </summary>
        Id Id { get; }

        /// <summary>
        /// This name is what is displayed in game.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the location of the entity
        /// </summary>
        Scene Scene{ get; set; }
    }
}