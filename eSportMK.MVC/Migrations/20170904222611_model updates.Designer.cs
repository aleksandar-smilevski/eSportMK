using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using eSportMK.MVC.Models;

namespace eSportMK.MVC.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170904222611_model updates")]
    partial class modelupdates
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eSportMK.MVC.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("eSportMK.MVC.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("eSportMK.MVC.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("eSportMK.MVC.Models.Location", b =>
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

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("eSportMK.MVC.Models.Match", b =>
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

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("eSportMK.MVC.Models.Player", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CountryId");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("FirstName");

                    b.Property<int?>("GameId");

                    b.Property<string>("LastName");

                    b.Property<string>("Nickname");

                    b.Property<string>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("GameId");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("eSportMK.MVC.Models.PlayerStatistics", b =>
                {
                    b.Property<string>("PlayerId");

                    b.Property<float>("Deaths");

                    b.Property<float>("Kills");

                    b.Property<int>("MatchesPlayed");

                    b.Property<float>("WinPercentage");

                    b.HasKey("PlayerId");

                    b.ToTable("PlayerStatistics");
                });

            modelBuilder.Entity("eSportMK.MVC.Models.Result", b =>
                {
                    b.Property<int>("MatchId");

                    b.Property<int>("ScoreTeam1");

                    b.Property<int>("ScoreTeam2");

                    b.Property<string>("TeamId");

                    b.HasKey("MatchId");

                    b.HasIndex("TeamId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("eSportMK.MVC.Models.Team", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CountryId");

                    b.Property<int?>("GameId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("GameId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("eSportMK.MVC.Models.TeamTournament", b =>
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

                    b.ToTable("TeamTournaments");
                });

            modelBuilder.Entity("eSportMK.MVC.Models.Tournament", b =>
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("eSportMK.MVC.Models.Match", b =>
                {
                    b.HasOne("eSportMK.MVC.Models.Game", "Game")
                        .WithMany("Matches")
                        .HasForeignKey("GameId");

                    b.HasOne("eSportMK.MVC.Models.Team", "Team1")
                        .WithMany()
                        .HasForeignKey("Team1Id");

                    b.HasOne("eSportMK.MVC.Models.Team", "Team2")
                        .WithMany()
                        .HasForeignKey("Team2Id");

                    b.HasOne("eSportMK.MVC.Models.Tournament", "Tournament")
                        .WithMany("Matches")
                        .HasForeignKey("TournamentId");
                });

            modelBuilder.Entity("eSportMK.MVC.Models.Player", b =>
                {
                    b.HasOne("eSportMK.MVC.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("eSportMK.MVC.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId");

                    b.HasOne("eSportMK.MVC.Models.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("eSportMK.MVC.Models.PlayerStatistics", b =>
                {
                    b.HasOne("eSportMK.MVC.Models.Player", "Player")
                        .WithOne("Statistics")
                        .HasForeignKey("eSportMK.MVC.Models.PlayerStatistics", "PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eSportMK.MVC.Models.Result", b =>
                {
                    b.HasOne("eSportMK.MVC.Models.Match", "Match")
                        .WithOne("Result")
                        .HasForeignKey("eSportMK.MVC.Models.Result", "MatchId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eSportMK.MVC.Models.Team")
                        .WithMany("Results")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("eSportMK.MVC.Models.Team", b =>
                {
                    b.HasOne("eSportMK.MVC.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("eSportMK.MVC.Models.Game", "Game")
                        .WithMany("Teams")
                        .HasForeignKey("GameId");
                });

            modelBuilder.Entity("eSportMK.MVC.Models.TeamTournament", b =>
                {
                    b.HasOne("eSportMK.MVC.Models.Team", "Team")
                        .WithMany("Tournaments")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eSportMK.MVC.Models.Tournament", "Tournament")
                        .WithMany("Teams")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eSportMK.MVC.Models.Tournament", b =>
                {
                    b.HasOne("eSportMK.MVC.Models.Game", "Game")
                        .WithMany("Tournaments")
                        .HasForeignKey("GameId");

                    b.HasOne("eSportMK.MVC.Models.Location", "Location")
                        .WithMany("Tournaments")
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("eSportMK.MVC.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("eSportMK.MVC.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eSportMK.MVC.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
