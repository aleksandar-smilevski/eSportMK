using System.ComponentModel.DataAnnotations;

namespace eSportMK.Data
{
    public class PlayerStatistics
    {
        [Key]
        public string PlayerId { get; set; }
        public float Kills { get; set; }
        public float Deaths { get; set; }
        public int MatchesPlayed { get; set; }
        public float WinPercentage { get; set; }
        public virtual Player Player { get; set; }
    }
}