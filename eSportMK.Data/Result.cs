using System.ComponentModel.DataAnnotations;

namespace eSportMK.Data
{
    public class Result
    {
        [Key]
        public int MatchId { get; set; }
        public int ScoreTeam1 { get; set; }
        public int ScoreTeam2 { get; set; }
        public virtual Match Match { get; set; }
    }
}