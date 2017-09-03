using System;

namespace eSportMK.MVC.Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public virtual Team Team1 { get; set; }
        public virtual Team Team2 { get; set; }
        public virtual Game Game { get; set; }
        public virtual Tournament Tournament { get; set; }
        public virtual Result Result { get; set; }
    }
}