using Data.DataProviders.Players;
using Data.Models.Entities;
using Data.Models.Entities.Humans;
using Data.Models.Exceptions;
using System.Collections.Generic;
using System;

namespace Data.Repositories
{
    public class PlayerRepository
    {
        private IPlayerDataProvider _dataProvider;
        private static Dictionary<string, Player> _data = new Dictionary<string, Player>();

        public PlayerRepository(IPlayerDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }
        //TODO: Inject DataAccessor here.
        //TODO: Cache
        /// <summary>
        /// Loads a player either from memory or from dataprovider if needed.
        /// Requires connection information to establish connection to client.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="connectionId"></param>
        /// <returns>A valid player</returns>
        /// <exception cref="TodoException">If no player exists or connection info is not provided.</exception>
        public Player Load(Id id, string connectionId)
        {
            //Todo: Ok, got some stuff to do in here
            //Basically we want a GetOrLoad as well.
            return _dataProvider.Get(id, connectionId);
        }

        /// <summary>
        /// Gets a Player from the Gamestate, does not look in dataprovider
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Player object from memory, Null of player is not loaded</returns>
        public Player Get(Id id)
        {
            //Todo: Ok, got some stuff to do in here
            if (_data.TryGetValue(id.Trunk, out Player value))
            {
                //updateCache
                return value;
            }
            else
            {
                return null;
            }
        }

        public Player Add(Player player)
        {
            //TODO: Maybe fix this a bit? How to handle player being added when they already exists? Not at all?
            if(_data.ContainsKey(player.Id.Trunk))
            {
                return null;
            }

            _data.Add(player.Id.Trunk, player);
            return player;
        }

        public void Remove(Player player)
        {
            throw new TodoException("Removing players is when User deletes a character");
        }

        public void Unload(Player actor)
        {
            _data.Remove(actor.Id.Trunk);
        }
    }
}
