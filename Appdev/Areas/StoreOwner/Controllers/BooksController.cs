using AppDev.Areas.StoreOwner.ViewModels;
using AppDev.Data;
using AppDev.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppDev.Areas.StoreOwner.Controllers
{
    [Area("StoreOwner")]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public BooksController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        // GET: StoreOwner/Books
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = context.Books.Include(b => b.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: StoreOwner/Books/Details/5
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

        // GET: StoreOwner/Books/Create
        public async Task<IActionResult> Create()
        {
            var model = new BookViewModel()
            {
                Categories = await context.Categories.ToListAsync(),
            };

            return View(model);
        }

        // POST: StoreOwner/Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var book = new Book()
                {
                    Title = model.Title,
                    Isbn = model.Isbn,
                    Description = model.Description,
                    Price = model.Price,
                    CategoryId = model.CategoryId,
                    StoreOwnerId = userManager.GetUserId(User),
                };
                context.Add(book);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            model.Categories = await context.Categories.ToListAsync();
            return View(model);
        }

        // GET: StoreOwner/Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || context.Books == null)
            {
                return NotFound();
            }

            var book = await context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var model = new BookViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                Isbn = book.Isbn,
                Description = book.Description,
                Price = book.Price,
                CategoryId = book.CategoryId,
                Categories = await context.Categories.ToListAsync(),
            };

            return View(model);
        }

        // POST: StoreOwner/Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var book = new Book()
                    {
                        Id = model.Id,
                        Title = model.Title,
                        Isbn = model.Isbn,
                        Description = model.Description,
                        Price = model.Price,
                        CategoryId = model.CategoryId,
                        StoreOwnerId = userManager.GetUserId(User),
                    };
                    context.Update(book);
                    await context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    if (!await (context.Books.AnyAsync(b => b.Id == model.Id)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cannot update book due to database error.");
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            model.Categories = await context.Categories.ToListAsync();
            return View(model);
        }

        // GET: StoreOwner/Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context.Books == null)
            {
                return NotFound();
            }

            var book = await context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: StoreOwner/Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (context.Books == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Books' is null.");
            }
            var book = await context.Books.FindAsync(id);
            if (book != null)
            {
                context.Books.Remove(book);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
