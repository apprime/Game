using Data.DataProviders.MySqlHelpers;
using Data.Models.Entities;
using Data.Models.Entities.Humans;
using MySql.Data.MySqlClient;
using System.Data;
using System.Threading.Tasks;

namespace Data.DataProviders.Players
{
    public class PersistentPlayerData : IPlayerDataProvider
    {
        private MySqlDb _db;

        public PersistentPlayerData(MySqlDb db)
        {
            _db = db;
        }

        public async Task<Player> Add(Player player)
        {
            var cmd = _db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO `player` (`name`, `lastLoggedOutPosition`) VALUES (@name, @lastLoggedOutPosition);";

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@name",
                DbType = DbType.String,
                Value = player.Name,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@lastLoggedOutPosition",
                DbType = DbType.String,
                Value = player.LoggedOutPosition,
            });

            await cmd.ExecuteNonQueryAsync();

            player.Id = Id.FromParts('P', player.LoggedOutPosition, cmd.LastInsertedId.ToString());

            return player;
        }

        public Task<Player> Get(Id playerId, string connectionId)
        {
            throw new System.NotImplementedException();
        }

        public Task Remove(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
