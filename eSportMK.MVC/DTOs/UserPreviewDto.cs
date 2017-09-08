using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSportMK.MVC.DTOs.Team
{
    public class UserPreviewDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Game { get; set; }
        public string Team { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<string> Roles { get; set; }
    }
}
