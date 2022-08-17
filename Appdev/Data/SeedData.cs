using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppDev.Data
{
    public class SeedData
    {
        public static void SeedRole(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole(Role.Admin),
                    new IdentityRole(Role.StoreOwner),
                    new IdentityRole(Role.Customer));
        }
    }
}