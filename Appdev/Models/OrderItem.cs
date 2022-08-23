using System.ComponentModel.DataAnnotations;

namespace AppDev.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        public double Price { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
