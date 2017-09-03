using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eSportMK.MVC.DTOs.Game
{
    public class CreateGameDto
    {
        [Required]
        public string Name { get; set; }
    }
}
