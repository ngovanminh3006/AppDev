using System.ComponentModel.DataAnnotations;

namespace AppDev.Areas.Admin.ViewModels
{
    public class NewCategoryRequestApprovalViewModel
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        public string StoreOwnerId { get; set; } = null!;

        public string? StoreOwnerFullName { get; set; }

        [Required]
        public bool? IsApproval { get; set; }

        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; } = null!;
    }
}
