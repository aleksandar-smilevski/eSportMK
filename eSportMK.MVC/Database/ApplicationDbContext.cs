using eSportMK.MVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eSportMK.MVC.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Tournament> Tournaments  { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerStatistics> PlayerStatistics { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamTournament> TeamTournaments { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
