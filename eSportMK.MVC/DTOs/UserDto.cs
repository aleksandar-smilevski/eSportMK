using System.ComponentModel.DataAnnotations;

namespace eSportMK.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
