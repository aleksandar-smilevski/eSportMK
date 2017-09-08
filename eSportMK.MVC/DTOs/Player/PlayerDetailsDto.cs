using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using eSportMK.MVC.DTOs.Country;
using eSportMK.MVC.DTOs.Team;

namespace eSportMK.MVC.DTOs.Player
{
    public class PlayerDetailsDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public int CountryId { get; set; }
        public int GameId { get; set; }
        public string TeamId { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
