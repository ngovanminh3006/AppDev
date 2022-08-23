using System.ComponentModel.DataAnnotations;

namespace AppDev.Models
{
    public class NewCategoryRequest
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        public string StoreOwnerId { get; set; } = null!;

        public ApplicationUser StoreOwner { get; set; } = null!;

        public bool? IsApproval { get; set; }

        [StringLength(200)]
        public string? Message { get; set; }
    }
}
