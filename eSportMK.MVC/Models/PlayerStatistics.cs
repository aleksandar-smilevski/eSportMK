using System.ComponentModel.DataAnnotations;

namespace eSportMK.MVC.Models
{
    public class PlayerStatistics
    {
        [Key]
        public int PlayerId { get; set; }
        public float Kills { get; set; }
        public float Deaths { get; set; }
        public int MatchesPlayed { get; set; }
        public float WinPercentage { get; set; }
        public virtual Player Player { get; set; }
    }
}