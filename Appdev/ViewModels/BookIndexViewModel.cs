using AppDev.Models;

namespace AppDev.ViewModels
{
    public class BookIndexViewModel
    {
        public ICollection<Book> Books { get; set; } = null!;

        public SearchViewModel SearchViewModel { get; set; } = new();
    }
}
