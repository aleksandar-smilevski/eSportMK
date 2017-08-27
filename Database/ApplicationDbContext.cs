using eSportMK.Data;
using Microsoft.EntityFrameworkCore;

namespace eSportMK.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tournament> Tournaments  { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerStatistics> PlayerStatistics { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamTournament> TeamTournaments { get; set; }
    }
}
