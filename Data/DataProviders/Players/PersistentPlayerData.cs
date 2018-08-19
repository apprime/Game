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
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();

            var trunk = (int)cmd.LastInsertedId;
            player.Id = Id.FromParts('P', player.LoggedOutPosition, trunk);

            return player;
        }

        public Player Get(Id playerId, string connectionId)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(Player player)
        {
            throw new System.NotImplementedException();
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@title",
                DbType = DbType.String,
                Value = Title,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@content",
                DbType = DbType.String,
                Value = Content,
            });
        }
    }
}
