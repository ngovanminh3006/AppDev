using AppDev.Data;
using AppDev.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppDev.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext context;

        public BooksController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index(SearchViewModel searchModel)
        {
            var query = context.Books.Include(b => b.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchModel.Keyword))
            {
                searchModel.Keyword = searchModel.Keyword.Trim();
                string keyword = searchModel.Keyword.ToLower();

                query = query
                    .Where(b => b.Title.ToLower().Contains(keyword)
                    || b.Category.Name.ToLower().Contains(keyword));
            }
            var books = await query.ToListAsync();

            var model = new BookIndexViewModel()
            {
                Books = books,
                SearchViewModel = searchModel,
            };

            return View(model);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || context.Books == null)
            {
                return NotFound();
            }

            var book = await context.Books
                .Include(b => b.Category)
                .Include(b => b.StoreOwner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
    }
}
