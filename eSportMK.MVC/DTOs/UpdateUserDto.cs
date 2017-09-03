using System;
using System.ComponentModel.DataAnnotations;

namespace eSportMK.Dto
{
    public class UpdateUserDto
    {
        [Required]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
