using eSportMK.MVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Linq;

namespace eSportMK.MVC.Database
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            try
            {
                context.Database.EnsureCreated();

                if (!context.Roles.Any())
                {
                    var roles = new IdentityRole[]
                    {
                    new IdentityRole("Admin"),
                    new IdentityRole("User")
                    };

                    context.Roles.AddRange(roles);
                    context.SaveChanges();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
