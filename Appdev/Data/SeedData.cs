using AppDev.Models;
using Microsoft.AspNetCore.Identity;

namespace AppDev.Data
{
    public class SeedData
    {
        public static async Task SeedRoleAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Role.Admin));
            await roleManager.CreateAsync(new IdentityRole(Role.StoreOwner));
            await roleManager.CreateAsync(new IdentityRole(Role.Customer));
        }

        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            var admin = new ApplicationUser("admin@g.c")
            {
                EmailConfirmed = true,
                Address = "",
                FullName = "Administrator",
                Email = "admin@g.c",
            };

            if (await userManager.FindByNameAsync(admin.UserName) == null)
            {
                await userManager.CreateAsync(admin, "Abc@1234");

                await userManager.AddToRoleAsync(admin, Role.Admin);
            }
        }
    }
}