using eSportMK.MVC.DTOs.Country;
using eSportMK.MVC.DTOs.Player;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eSportMK.MVC.DTOs.Team
{
    public class TeamDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Country { get; set; }
        [Required]
        public string Game { get; set; }
        public IEnumerable<string> Players { get; set; }
    }
}
