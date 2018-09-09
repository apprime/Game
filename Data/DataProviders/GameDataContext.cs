using Data.Models.Entities.Humans;
using Microsoft.EntityFrameworkCore;

namespace Data.DataProviders
{
    public class GameDataContext : DbContext
    {
        public GameDataContext(DbContextOptions<GameDataContext> options)
            : base(options) { }

        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().ToTable("Player");
            modelBuilder.ApplyConfiguration(new PlayerEntityConfiguration());
        }
    }
}
    