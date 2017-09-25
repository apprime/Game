using Data.Models.Entities;
using Data.Models.Entities.Humans;
using Data.Models.Exceptions;

namespace Data.Repositories
{
    public class PlayerRepository
    {
        //TODO: Inject DataAccessor here.
        //TODO: Cache
        public Player Get(Id id, string connectionId)
        {
            //Todo: Ok, got some stuff to do in here
            return new Player("P123/PlayerMcPlayerface/1/"+connectionId);
        }

        public Player Add(string id)
        {
            throw new TodoException("Adding players is when User creates a character");
        }

        public void Remove(string id)
        {
            throw new TodoException("Removing players is when User deletes a character");
        }
    }
}
