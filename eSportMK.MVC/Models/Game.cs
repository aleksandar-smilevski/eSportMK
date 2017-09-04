using System.Collections.Generic;

namespace eSportMK.MVC.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Team> Teams { get; set; }
        public virtual IEnumerable<Tournament> Tournaments { get; set; }
        public virtual IEnumerable<Match> Matches { get; set; }
    }
}