using Data.DataProviders.Players;
using Data.Models.Entities;
using Data.Models.Entities.Humans;
using Data.Models.Exceptions;

namespace Data.Repositories
{
    public class PlayerRepository
    {
        private IPlayerDataProvider _dataProvider;

        public PlayerRepository(IPlayerDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }
        //TODO: Inject DataAccessor here.
        //TODO: Cache
        public Player Get(Id id, string connectionId)
        {
            //Todo: Ok, got some stuff to do in here
            return _dataProvider.Get(id, connectionId);
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
