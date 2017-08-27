using System.ComponentModel.DataAnnotations;

namespace eSportMK.Data
{
    public class TeamTournament
    {
        public int Id { get; set; }
        [Required]
        public string TeamId { get; set; }
        [Required]
        public string TournamentId { get; set; }
        public virtual Team Team { get; set; }
        public virtual Tournament Tournament { get; set; }
    }
}