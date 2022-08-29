using AppDev.Models;
using AppDev.ViewModels;

namespace AppDev.Areas.StoreOwner.ViewModels
{
    public class OrderIndexViewModel
    {
        public ICollection<Order> Orders { get; set; } = null!;

        public SearchViewModel SearchViewModel { get; set; } = new();
    }
}
