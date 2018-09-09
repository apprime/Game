using Data.Models.Entities.Humans;
using Microsoft.EntityFrameworkCore;

namespace Data.DataProviders
{
    public class GameDataContext : DbContext
    {
        public GameDataContext(DbContextOptions<GameDataContext> options)
            : base(options) { }

        DbSet<Player> Players { get; set; }
    }
}
    