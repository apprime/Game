using Data.Models.Entities;
using Data.Models.Entities.Humans;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Data.DataProviders.Players
{
    public class PersistentPlayerData : IPlayerDataProvider
    {
        private GameDataContext _ctx;

        public PersistentPlayerData(GameDataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Player> Add(Player player)
        {
            //Todo: AddAsync should be used if you generate values with triggers in db.
            //      Probably won't be needed here, but adding a todo for later cleanup of all ctx adds
            _ctx.Players.Add(player);
            await _ctx.SaveChangesAsync();

            return player;
        }

        public Task<Player> Get(Id playerId, string connectionId)
        {
            return _ctx.Players.SingleOrDefaultAsync(p => p.Id == playerId)
                ?? throw new ArgumentException("No such player in database");
        }

        public Task Remove(Player player)
        {
            throw new System.NotImplementedException();
        }
    }
}
