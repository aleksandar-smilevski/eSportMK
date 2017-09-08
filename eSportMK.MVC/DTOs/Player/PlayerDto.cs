using eSportMK.MVC.DTOs.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSportMK.MVC.DTOs.Player
{
    public class PlayerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string Country { get; set; }
        public string Game { get; set; }
        public string Team { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
