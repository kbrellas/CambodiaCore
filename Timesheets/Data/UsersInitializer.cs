using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.Models;

namespace Timesheets.Data
{
    public static class UsersInitializer
    {
        public static void SeedUsers(UserManager<MyUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@test.com").Result == null)
            {
                MyUser user = new MyUser
                {
                    UserName = "admin@test.com",
                    Email = "admin@test.com",
                    EmailConfirmed = true,
                    CostPerHour = 2,
                    FirstName="admin",
                    LastName="Admin"
                };

                IdentityResult result = userManager.CreateAsync(user, "123456").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();

                }
            }
            if (userManager.FindByEmailAsync("user1@test.com").Result == null) {
                MyUser user = new MyUser
                {
                    UserName = "user1@test.com",
                    Email = "user1@test.com",
                    EmailConfirmed = true,
                    CostPerHour = 3,
                    FirstName = "John",
                    LastName = "Doe"
                };

                IdentityResult result = userManager.CreateAsync(user, "123456").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Employee").Wait();

                }

            }
        }
    }
}
