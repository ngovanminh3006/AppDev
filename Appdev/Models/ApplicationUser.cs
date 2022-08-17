using Microsoft.AspNetCore.Identity;

namespace AppDev.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base() { }

        public ApplicationUser(string userName) : base(userName) { }

        public string Address { get; set; } = null!;
    }
}
