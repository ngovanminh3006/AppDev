using System.ComponentModel.DataAnnotations;

namespace AppDev.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string CustomerId { get; set; } = null!;
        public ApplicationUser Customer { get; set; } = null!;

        public string StoreOwnerId { get; set; } = null!;
        public ApplicationUser StoreOwner { get; set; } = null!;

        public ICollection<OrderItem> Items { get; set; } = null!;
    }
}
