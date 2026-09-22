using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SistemaReserva.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Infrastructure.Identity
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var roleManager =
                services.GetRequiredService<RoleManager<IdentityRole>>();

            var userManager =
                services.GetRequiredService<UserManager<ApplicationUser>>();

            const string adminRole = "Admin";
            const string adminEmail = "admin@sistema.com";
            const string adminPassword = "Admin@123456";

            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                var roleResult =
                    await roleManager.CreateAsync(new IdentityRole(adminRole));

                if (!roleResult.Succeeded)
                {
                    throw new Exception(
                        string.Join(", ",
                            roleResult.Errors.Select(e => e.Description)));
                }
            }

            var admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var userResult =
                    await userManager.CreateAsync(admin, adminPassword);

                if (!userResult.Succeeded)
                {
                    throw new Exception(
                        string.Join(", ",
                            userResult.Errors.Select(e => e.Description)));
                }
            }

            if (!await userManager.IsInRoleAsync(admin, adminRole))
            {
                var roleResult =
                    await userManager.AddToRoleAsync(admin, adminRole);

                if (!roleResult.Succeeded)
                {
                    throw new Exception(
                        string.Join(", ",
                            roleResult.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
