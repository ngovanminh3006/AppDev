using AppDev.Models;

namespace AppDev.ViewModels
{
    public class CartViewModel
    {
        public ApplicationUser User { get; set; } = null!;

        public ICollection<CartItemViewModel> CartItems { get; set; } = null!;

        public double TotalPrice { get; set; }

        public double TotalQuantity { get; set; }
    }
}
