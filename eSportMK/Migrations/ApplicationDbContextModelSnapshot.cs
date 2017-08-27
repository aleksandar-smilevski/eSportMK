using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using eSportMK.Database;
using eSportMK.Data;

namespace eSportMK.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eSportMK.Data.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("eSportMK.Data.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("Latitude")
                        .IsRequired();

                    b.Property<string>("Longitude")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("eSportMK.Data.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int?>("GameId");

                    b.Property<string>("Team1Id");

                    b.Property<string>("Team2Id");

                    b.Property<string>("TournamentId");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("Team1Id");

                    b.HasIndex("Team2Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("eSportMK.Data.Player", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Country");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("eSportMK.Data.PlayerStatistics", b =>
                {
                    b.Property<string>("PlayerId");

                    b.Property<float>("Deaths");

                    b.Property<float>("Kills");

                    b.Property<int>("MatchesPlayed");

                    b.Property<float>("WinPercentage");

                    b.HasKey("PlayerId");

                    b.ToTable("PlayerStatistics");
                });

            modelBuilder.Entity("eSportMK.Data.Result", b =>
                {
                    b.Property<int>("MatchId");

                    b.Property<int>("ScoreTeam1");

                    b.Property<int>("ScoreTeam2");

                    b.Property<string>("TeamId");

                    b.HasKey("MatchId");

                    b.HasIndex("TeamId");

                    b.ToTable("Result");
                });

            modelBuilder.Entity("eSportMK.Data.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("eSportMK.Data.Team", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Country");

                    b.Property<int?>("GameId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("eSportMK.Data.TeamTournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TeamId")
                        .IsRequired();

                    b.Property<string>("TournamentId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.HasIndex("TournamentId");

                    b.ToTable("TeamTournament");
                });

            modelBuilder.Entity("eSportMK.Data.Tournament", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<int?>("GameId");

                    b.Property<int?>("LocationId");

                    b.Property<string>("Name");

                    b.Property<decimal>("PrizePool");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("LocationId");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("eSportMK.Data.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsActive");

                    b.Property<string>("LastName");

                    b.Property<string>("PasswordHash");

                    b.Property<Guid?>("RoleId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("eSportMK.Data.Match", b =>
                {
                    b.HasOne("eSportMK.Data.Game", "Game")
                        .WithMany("Matches")
                        .HasForeignKey("GameId");

                    b.HasOne("eSportMK.Data.Team", "Team1")
                        .WithMany()
                        .HasForeignKey("Team1Id");

                    b.HasOne("eSportMK.Data.Team", "Team2")
                        .WithMany()
                        .HasForeignKey("Team2Id");

                    b.HasOne("eSportMK.Data.Tournament", "Tournament")
                        .WithMany("Matches")
                        .HasForeignKey("TournamentId");
                });

            modelBuilder.Entity("eSportMK.Data.Player", b =>
                {
                    b.HasOne("eSportMK.Data.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("eSportMK.Data.PlayerStatistics", b =>
                {
                    b.HasOne("eSportMK.Data.Player", "Player")
                        .WithOne("Statistics")
                        .HasForeignKey("eSportMK.Data.PlayerStatistics", "PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eSportMK.Data.Result", b =>
                {
                    b.HasOne("eSportMK.Data.Match", "Match")
                        .WithOne("Result")
                        .HasForeignKey("eSportMK.Data.Result", "MatchId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eSportMK.Data.Team")
                        .WithMany("Results")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("eSportMK.Data.Team", b =>
                {
                    b.HasOne("eSportMK.Data.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId");
                });

            modelBuilder.Entity("eSportMK.Data.TeamTournament", b =>
                {
                    b.HasOne("eSportMK.Data.Team", "Team")
                        .WithMany("Tournaments")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eSportMK.Data.Tournament", "Tournament")
                        .WithMany("Teams")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eSportMK.Data.Tournament", b =>
                {
                    b.HasOne("eSportMK.Data.Game", "Game")
                        .WithMany("Tournaments")
                        .HasForeignKey("GameId");

                    b.HasOne("eSportMK.Data.Location", "Location")
                        .WithMany("Tournaments")
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("eSportMK.Data.User", b =>
                {
                    b.HasOne("eSportMK.Data.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");
                });
        }
    }
}
