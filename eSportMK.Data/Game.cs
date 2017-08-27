using System.Collections.Generic;

namespace eSportMK.Data
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Tournament> Tournaments { get; set; }
        public virtual IEnumerable<Match> Matches { get; set; }
    }
}