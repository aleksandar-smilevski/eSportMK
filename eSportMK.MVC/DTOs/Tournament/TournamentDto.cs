using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eSportMK.MVC.DTOs.Location;
using eSportMK.MVC.Models;

namespace eSportMK.MVC.DTOs.Tournament
{
    public class TournamentDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal PrizePool { get; set; }
        public string Game { get; set; }
        public LocationDto Location { get; set; }
        public TournamentType Type { get; set; }
    }
}
