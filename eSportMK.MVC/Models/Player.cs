using System;
using System.ComponentModel.DataAnnotations;

namespace eSportMK.MVC.Models
{
    public class Player
    {
        [Key]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName  { get; set; }
        public string Nickname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string TeamId { get; set; }
        public virtual Team Team { get; set; }
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}