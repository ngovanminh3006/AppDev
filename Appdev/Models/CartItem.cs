using System.ComponentModel.DataAnnotations;

namespace AppDev.Models
{
    // composite primary key
    public class CartItem
    {
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; } = 1;
    }
}
