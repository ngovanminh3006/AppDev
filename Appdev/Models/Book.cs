using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppDev.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string StoreOwnerId { get; set; } = null!;
        public ApplicationUser StoreOwner { get; set; } = null!;

        [StringLength(100)]
        public string Title { get; set; } = null!;

        [StringLength(20)]
        public string? Isbn { get; set; } = null!;

        [StringLength(1000)]
        public string Description { get; set; } = null!;

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;
    }
}
