﻿using eSportMK.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSportMK.MVC.Database
{
    public static class DbInitializer
    {

        public static async Task InitializeAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            try
            {

                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("User"));
                }

                if (!userManager.Users.Any())
                {
                    var appUser = new ApplicationUser() {UserName = "Admin", Email = "a@a.com"};
                    var res = await userManager.CreateAsync(appUser, "Admin123!");
                    res = await userManager.AddToRoleAsync(appUser, "Admin");
                }

                if (!context.Games.Any())
                {
                    var games = new[]
                    {
                        new Game { Name = "Dota 2"},
                        new Game { Name = "CS:GO"},
                        new Game { Name = "League of Legends"},
                        new Game { Name = "Overwatch"}
                    };

                    context.Games.AddRange(games);
                    context.SaveChanges();
                }

                if (!context.Countries.Any())
                {
                    var countries = new[]
                    {
                        new Country{Name = "Afghanistan"},
                        new Country{Name = "Åland Islands"},
                        new Country{Name = "Albania"},
                        new Country{Name = "Algeria"},
                        new Country{Name = "American Samoa"},
                        new Country{Name = "AndorrA"},
                        new Country{Name = "Angola"},
                        new Country{Name = "Anguilla"},
                        new Country{Name = "Antarctica"},
                        new Country{Name = "Antigua and Barbuda"},
                        new Country{Name = "Argentina"},
                        new Country{Name = "Armenia"},
                        new Country{Name = "Aruba"},
                        new Country{Name = "Australia"},
                        new Country{Name = "Austria"},
                        new Country{Name = "Azerbaijan"},
                        new Country{Name = "Bahamas"},
                        new Country{Name = "Bahrain"},
                        new Country{Name = "Bangladesh"},
                        new Country{Name = "Barbados"},
                        new Country{Name = "Belarus"},
                        new Country{Name = "Belgium"},
                        new Country{Name = "Belize"},
                        new Country{Name = "Benin"},
                        new Country{Name = "Bermuda"},
                        new Country{Name = "Bhutan"},
                        new Country{Name = "Bolivia"},
                        new Country{Name = "Bosnia and Herzegovina"},
                        new Country{Name = "Botswana"},
                        new Country{Name = "Bouvet Island"},
                        new Country{Name = "Brazil"},
                        new Country{Name = "British Indian Ocean Territory"},
                        new Country{Name = "Brunei Darussalam"},
                        new Country{Name = "Bulgaria"},
                        new Country{Name = "Burkina Faso"},
                        new Country{Name = "Burundi"},
                        new Country{Name = "Cambodia"},
                        new Country{Name = "Cameroon"},
                        new Country{Name = "Canada"},
                        new Country{Name = "Cape Verde"},
                        new Country{Name = "Cayman Islands"},
                        new Country{Name = "Central African Republic"},
                        new Country{Name = "Chad"},
                        new Country{Name = "Chile"},
                        new Country{Name = "China"},
                        new Country{Name = "Christmas Island"},
                        new Country{Name = "Cocos (Keeling) Islands"},
                        new Country{Name = "Colombia"},
                        new Country{Name = "Comoros"},
                        new Country{Name = "Congo"},
                        new Country{Name = "Congo, The Democratic Republic of the"},
                        new Country{Name = "Cook Islands"},
                        new Country{Name = "Costa Rica"},
                        new Country{Name = "Cote D'Ivoire"},
                        new Country{Name = "Croatia"},
                        new Country{Name = "Cuba"},
                        new Country{Name = "Cyprus"},
                        new Country{Name = "Czech Republic"},
                        new Country{Name = "Denmark"},
                        new Country{Name = "Djibouti"},
                        new Country{Name = "Dominica"},
                        new Country{Name = "Dominican Republic"},
                        new Country{Name = "Ecuador"},
                        new Country{Name = "Egypt"},
                        new Country{Name = "El Salvador"},
                        new Country{Name = "Equatorial Guinea"},
                        new Country{Name = "Eritrea"},
                        new Country{Name = "Estonia"},
                        new Country{Name = "Ethiopia"},
                        new Country{Name = "Falkland Islands (Malvinas)"},
                        new Country{Name = "Faroe Islands"},
                        new Country{Name = "Fiji"},
                        new Country{Name = "Finland"},
                        new Country{Name = "France"},
                        new Country{Name = "French Guiana"},
                        new Country{Name = "French Polynesia"},
                        new Country{Name = "French Southern Territories"},
                        new Country{Name = "Gabon"},
                        new Country{Name = "Gambia"},
                        new Country{Name = "Georgia"},
                        new Country{Name = "Germany"},
                        new Country{Name = "Ghana"},
                        new Country{Name = "Gibraltar"},
                        new Country{Name = "Greece"},
                        new Country{Name = "Greenland"},
                        new Country{Name = "Grenada"},
                        new Country{Name = "Guadeloupe"},
                        new Country{Name = "Guam"},
                        new Country{Name = "Guatemala"},
                        new Country{Name = "Guernsey"},
                        new Country{Name = "Guinea"},
                        new Country{Name = "Guinea-Bissau"},
                        new Country{Name = "Guyana"},
                        new Country{Name = "Haiti"},
                        new Country{Name = "Heard Island and Mcdonald Islands"},
                        new Country{Name = "Holy See (Vatican City State)"},
                        new Country{Name = "Honduras"},
                        new Country{Name = "Hong Kong"},
                        new Country{Name = "Hungary"},
                        new Country{Name = "Iceland"},
                        new Country{Name = "India"},
                        new Country{Name = "Indonesia"},
                        new Country{Name = "Iran, Islamic Republic Of"},
                        new Country{Name = "Iraq"},
                        new Country{Name = "Ireland"},
                        new Country{Name = "Isle of Man"},
                        new Country{Name = "Israel"},
                        new Country{Name = "Italy"},
                        new Country{Name = "Jamaica"},
                        new Country{Name = "Japan"},
                        new Country{Name = "Jersey"},
                        new Country{Name = "Jordan"},
                        new Country{Name = "Kazakhstan"},
                        new Country{Name = "Kenya"},
                        new Country{Name = "Kiribati"},
                        new Country{Name = "Korea, Democratic People's Republic of"},
                        new Country{Name = "Korea, Republic of"},
                        new Country{Name = "Kuwait"},
                        new Country{Name = "Kyrgyzstan"},
                        new Country{Name = "Lao People's Democratic Republic"},
                        new Country{Name = "Latvia"},
                        new Country{Name = "Lebanon"},
                        new Country{Name = "Lesotho"},
                        new Country{Name = "Liberia"},
                        new Country{Name = "Libyan Arab Jamahiriya"},
                        new Country{Name = "Liechtenstein"},
                        new Country{Name = "Lithuania"},
                        new Country{Name = "Luxembourg"},
                        new Country{Name = "Macao"},
                        new Country{Name = "Macedonia"},
                        new Country{Name = "Madagascar"},
                        new Country{Name = "Malawi"},
                        new Country{Name = "Malaysia"},
                        new Country{Name = "Maldives"},
                        new Country{Name = "Mali"},
                        new Country{Name = "Malta"},
                        new Country{Name = "Marshall Islands"},
                        new Country{Name = "Martinique"},
                        new Country{Name = "Mauritania"},
                        new Country{Name = "Mauritius"},
                        new Country{Name = "Mayotte"},
                        new Country{Name = "Mexico"},
                        new Country{Name = "Micronesia, Federated States of"},
                        new Country{Name = "Moldova, Republic of"},
                        new Country{Name = "Monaco"},
                        new Country{Name = "Mongolia"},
                        new Country{Name = "Montenegro"},
                        new Country{Name = "Montserrat"},
                        new Country{Name = "Morocco"},
                        new Country{Name = "Mozambique"},
                        new Country{Name = "Myanmar"},
                        new Country{Name = "Namibia"},
                        new Country{Name = "Nauru"},
                        new Country{Name = "Nepal"},
                        new Country{Name = "Netherlands"},
                        new Country{Name = "Netherlands Antilles"},
                        new Country{Name = "New Caledonia"},
                        new Country{Name = "New Zealand"},
                        new Country{Name = "Nicaragua"},
                        new Country{Name = "Niger"},
                        new Country{Name = "Nigeria"},
                        new Country{Name = "Niue"},
                        new Country{Name = "Norfolk Island"},
                        new Country{Name = "Northern Mariana Islands"},
                        new Country{Name = "Norway"},
                        new Country{Name = "Oman"},
                        new Country{Name = "Pakistan"},
                        new Country{Name = "Palau"},
                        new Country{Name = "Palestinian Territory, Occupied"},
                        new Country{Name = "Panama"},
                        new Country{Name = "Papua New Guinea"},
                        new Country{Name = "Paraguay"},
                        new Country{Name = "Peru"},
                        new Country{Name = "Philippines"},
                        new Country{Name = "Pitcairn"},
                        new Country{Name = "Poland"},
                        new Country{Name = "Portugal"},
                        new Country{Name = "Puerto Rico"},
                        new Country{Name = "Qatar"},
                        new Country{Name = "Reunion"},
                        new Country{Name = "Romania"},
                        new Country{Name = "Russian Federation"},
                        new Country{Name = "RWANDA"},
                        new Country{Name = "Saint Helena"},
                        new Country{Name = "Saint Kitts and Nevis"},
                        new Country{Name = "Saint Lucia"},
                        new Country{Name = "Saint Pierre and Miquelon"},
                        new Country{Name = "Saint Vincent and the Grenadines"},
                        new Country{Name = "Samoa"},
                        new Country{Name = "San Marino"},
                        new Country{Name = "Sao Tome and Principe"},
                        new Country{Name = "Saudi Arabia"},
                        new Country{Name = "Senegal"},
                        new Country{Name = "Serbia"},
                        new Country{Name = "Seychelles"},
                        new Country{Name = "Sierra Leone"},
                        new Country{Name = "Singapore"},
                        new Country{Name = "Slovakia"},
                        new Country{Name = "Slovenia"},
                        new Country{Name = "Solomon Islands"},
                        new Country{Name = "Somalia"},
                        new Country{Name = "South Africa"},
                        new Country{Name = "South Georgia and the South Sandwich Islands"},
                        new Country{Name = "Spain"},
                        new Country{Name = "Sri Lanka"},
                        new Country{Name = "Sudan"},
                        new Country{Name = "Suriname"},
                        new Country{Name = "Svalbard and Jan Mayen"},
                        new Country{Name = "Swaziland"},
                        new Country{Name = "Sweden"},
                        new Country{Name = "Switzerland"},
                        new Country{Name = "Syrian Arab Republic"},
                        new Country{Name = "Taiwan, Province of China"},
                        new Country{Name = "Tajikistan"},
                        new Country{Name = "Tanzania, United Republic of"},
                        new Country{Name = "Thailand"},
                        new Country{Name = "Timor-Leste"},
                        new Country{Name = "Togo"},
                        new Country{Name = "Tokelau"},
                        new Country{Name = "Tonga"},
                        new Country{Name = "Trinidad and Tobago"},
                        new Country{Name = "Tunisia"},
                        new Country{Name = "Turkey"},
                        new Country{Name = "Turkmenistan"},
                        new Country{Name = "Turks and Caicos Islands"},
                        new Country{Name = "Tuvalu"},
                        new Country{Name = "Uganda"},
                        new Country{Name = "Ukraine"},
                        new Country{Name = "United Arab Emirates"},
                        new Country{Name = "United Kingdom"},
                        new Country{Name = "United States"},
                        new Country{Name = "United States Minor Outlying Islands"},
                        new Country{Name = "Uruguay"},
                        new Country{Name = "Uzbekistan"},
                        new Country{Name = "Vanuatu"},
                        new Country{Name = "Venezuela"},
                        new Country{Name = "Viet Nam"},
                        new Country{Name = "Virgin Islands, British"},
                        new Country{Name = "Virgin Islands, U.S."},
                        new Country{Name = "Wallis and Futuna"},
                        new Country{Name = "Western Sahara"},
                        new Country{Name = "Yemen"},
                        new Country{Name = "Zambia"},
                        new Country{Name = "Zimbabwe"}
                    };

                    context.Countries.AddRange(countries);
                    context.SaveChanges();
                }

                if (!context.Teams.Any())
                {
                    var teams = new[]
                    {
                        new Team { Name = "SK Gaming", Country = context.Countries.FirstOrDefault(x => x.Name.Equals("Brazil")), Game = context.Games.FirstOrDefault(x => x.Name.Equals("CS:GO")) }
                    };

                    context.AddRange(teams);
                    context.SaveChanges();
                }

                if (!context.Players.Any())
                {
                    var players = new[]
                    {
                        new Player { FirstName = "Gabriel", LastName = "Toledo", Nickname = "FalleN", DateOfBirth = new DateTime(1991,5,30),
                            Country = context.Countries.FirstOrDefault(x => x.Name.Equals("Brazil")),
                                Game = context.Games.FirstOrDefault(x => x.Name.Equals("CS:GO")),
                                    Team = context.Teams.FirstOrDefault(x => x.Name.Equals("SK Gaming")) }
                    };

                    context.AddRange(players);
                    context.SaveChanges();
                }

                //if (!context.Tournaments.Any())
                //{
                //    var tournaments = new List<Tournament>()
                //    {
                //        new Tournament()
                //        {
                //            Id = Guid.NewGuid().ToString(),
                //            Name = "DreamHack Masters Malmö 2017",
                //            Date = new DateTime(2017, 8, 30),
                //            Description = "Major CS:GO tournamnet in Sweden",
                //            PrizePool = new decimal(250000),
                //            Type = TournamentType.Major,
                //            GameId = context.Games.FirstOrDefault(x => x.Name == "CS:GO").Id,
                //            LocationId = context.Locations.FirstOrDefault(x => x.City == "Malmö").Id
                //        }
                //    };

                //    context.AddRange(tournaments);
                //    context.SaveChanges();
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
