using System;
using System.ComponentModel.DataAnnotations;

namespace eSportMK.Data
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid();
            IsActive = true;
        }

        public User(string userName)
        {
            Id = Guid.NewGuid();
            IsActive = true;
            UserName = userName;
        }
        [Key]
        public Guid Id { get; private set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public virtual Role Role { get; set; }
    }
}
