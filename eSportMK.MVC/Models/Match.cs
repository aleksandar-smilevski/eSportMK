using System;

namespace eSportMK.MVC.Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Team1Id { get; set; }
        public string Team2Id { get; set; }
        public MatchType MatchType { get; set; }
        public int GameId { get; set; }
        public string TournamentId { get; set; }
        public int ResultId { get; set; }
        public virtual Team Team1 { get; set; }
        public virtual Team Team2 { get; set; }
        public virtual Game Game { get; set; }
        public virtual Tournament Tournament { get; set; }
        public virtual Result Result { get; set; }
    }

    public enum MatchType
    {
        B01, B03, B05
    }
}