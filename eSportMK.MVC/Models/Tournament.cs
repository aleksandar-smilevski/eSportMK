using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eSportMK.MVC.Models
{
    public class Tournament
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal PrizePool { get; set; }
        public virtual TournamentType Type { get; set; }
        
        public int GameId { get; set; }
        public int LocationId { get; set; }
        
        public virtual Location Location { get; set; }
        public virtual IEnumerable<TeamTournament> Teams { get; set; }
        public virtual IEnumerable<Match> Matches { get; set; }
        public virtual Game Game { get; set; }

    }

    public enum TournamentType
    {
        Major, InternationalLan, Minor, Other 
    }
}
