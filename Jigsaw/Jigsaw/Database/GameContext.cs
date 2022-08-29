using Jigsaw.Models;
using Microsoft.EntityFrameworkCore;

namespace Jigsaw.Database
{
    public class GameContext : DbContext
    {

        public GameContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=Jigsaw.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasKey(e => e.Id); //auto increment later

            modelBuilder.Entity<Game>()
                .Property(b => b._answer).HasColumnName("Answer");

            modelBuilder.Entity<Game>()
                .Property(b => b._currentState).HasColumnName("CurrentState");
        }

        public DbSet<Game> Games { get; set; }
    }
}
