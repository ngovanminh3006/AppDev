using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        public static async Task SeedUsersAsync(UserManager<IdentityUser> userManager)
        {
            var admin = new IdentityUser()
            {
                UserName = "admin@g.c",
                Email = "admin@g.c",
                EmailConfirmed = true,
            };

            if (await userManager.FindByNameAsync(admin.UserName) == null)
            {
                await userManager.CreateAsync(admin, "Abc@1234");

                await userManager.AddToRoleAsync(admin, Role.Admin);
            }
        }
    }
}