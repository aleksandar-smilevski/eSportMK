
namespace eSportMK.MVC.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string City { get; set; }
        public int CountryId { get; set; }
        public string Address { get; set; }
        public virtual Country Country { get; set; }
    }
}