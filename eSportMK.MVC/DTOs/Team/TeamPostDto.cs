using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSportMK.MVC.DTOs.Player;
using eSportMK.MVC.Models;

namespace eSportMK.MVC.DTOs.Team
{
    public class TeamPostDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int GameId { get; set; }
        public int CountryId { get; set; }
    }
}
