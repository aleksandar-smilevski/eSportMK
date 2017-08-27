using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eSportMK.Data
{
    public class Role
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<User> Users { get; set; }
    }
}
