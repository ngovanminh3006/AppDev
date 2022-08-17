using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AppDev.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base() { }

        public ApplicationUser(string userName) : base(userName) { }

        [Display(Name = "Full name")]
        [StringLength(100)]
        public string FullName { get; set; } = null!;

        [StringLength(200)]
        public string Address { get; set; } = null!;
    }
}
