using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eSportMK.MVC.Models
{
    public class Location
    {
        public int Id { get; set; }
        [Required]
        public string Latitude { get; set; }
        [Required]
        public string Longitude { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public virtual IEnumerable<Tournament> Tournaments { get; set; }
    }
}