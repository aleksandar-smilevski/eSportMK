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
        public CountryDto Country { get; set; }
        [Required]
        public GameDto Game { get; set; }
        public IEnumerable<PlayerDto> Players { get; set; }
    }
}
