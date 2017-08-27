﻿using System.Collections;
using System.Collections.Generic;

namespace eSportMK.Data
{
    public class Team
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public virtual IEnumerable<Result> Results { get; set; }
        public virtual IEnumerable<Player> Players { get; set; }
        public virtual IEnumerable<TeamTournament> Tournaments { get; set; }
        public virtual Game Game { get; set; }
    }
}