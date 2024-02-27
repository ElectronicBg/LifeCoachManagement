using LifeCoachManagement.Models;
using Microsoft.AspNetCore.Identity;

namespace LifeCoachManagement.Data
{
    public class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            //Seed Roles
            var userManager = service.GetService<UserManager<ApplicationUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();

            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Coach.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Client.ToString()));

            // creating admin

            var admin = new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                Name = "Beloslava",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var coach = new ApplicationUser
            {
                UserName = "coach@gmail.com",
                Email = "coach@gmail.com",
                Name = "Vasko",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var adminInDb = await userManager.FindByEmailAsync(admin.Email);
            if (adminInDb == null)
            {
                await userManager.CreateAsync(admin, "Admin@123");
                await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
            }

            var coachInDb = await userManager.FindByEmailAsync(coach.Email);
            if (coachInDb == null)
            {
                await userManager.CreateAsync(coach, "Coach@123");
                await userManager.AddToRoleAsync(coach, Roles.Coach.ToString());
            }
        }
    }
}
