using AppDev.Models;
using System.ComponentModel.DataAnnotations;

namespace AppDev.ViewModels
{
    public class CartItemViewModel
    {
        public int BookId { get; set; }

        public string BookTitle { get; set; } = null!;

        public ApplicationUser StoreOwner { get; set; } = null!;

        public string UserId { get; set; } = null!;

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; } = 1;

        public double Price { get; set; }
        public double TotalPrice { get; set; }
    }
}
