using AppDev.Data;
using AppDev.Models;
using AppDev.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppDev.Controllers
{
    [Authorize(Roles = Role.Customer)]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        
        private string GetCurrentUserId()
        {
            return userManager.GetUserId(User);
        }

        public CartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();

            var cartItems = await context.CartItems
                .Include(i => i.Book)
                .ThenInclude(b => b.StoreOwner)
                .Where(i => i.UserId == userId)
                .Select(i => new CartItemViewModel()
                {
                    BookId = i.BookId,
                    BookTitle = i.Book.Title,
                    StoreOwner = i.Book.StoreOwner,
                    Quantity = i.Quantity,
                    Price = i.Book.Price,
                    TotalPrice = i.Book.Price * i.Quantity,
                    Description = i.Book.Description,
                    Category = i.Book.Category, 
                })
                .ToListAsync();

            var cart = new CartViewModel()
            {
                User = await userManager.GetUserAsync(User),
                CartItems = cartItems,
                TotalPrice = cartItems.Sum(i => i.TotalPrice),
                TotalQuantity = cartItems.Sum(i => i.Quantity),
            };

            return View(cart);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = GetCurrentUserId();
            var item = await context.CartItems.FirstOrDefaultAsync(i => i.UserId == userId && i.BookId == id);

            if (item == null)
                return RedirectToAction(nameof(Index));

            try
            {
                context.Remove(item);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Problem(ex.InnerException?.Message ?? ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Add(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = GetCurrentUserId();
            var item = await context.CartItems.FirstOrDefaultAsync(i => i.UserId == userId && i.BookId == id);

            if (item == null)
            {
                item = new CartItem()
                {
                    UserId = GetCurrentUserId(),
                    BookId = id.Value,
                    Quantity = 1
                };
                context.Add(item);
            }
            else
            {
                item.Quantity++;
                context.Update(item);
            }

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Problem(ex.InnerException?.Message ?? ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            return await Index();
        }

        [HttpPost]
        [ActionName(nameof(Checkout))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckoutConfirm()
        {
            var currentUserId = GetCurrentUserId();

            var cartItems = await context.CartItems
                .AsNoTracking()
                .Include(i => i.Book)
                .ThenInclude(b => b.StoreOwner)
                .Where(i => i.UserId == currentUserId)
                .ToListAsync();

            var ordersQuery = cartItems
                .GroupBy(i => i.Book.StoreOwnerId, (id, items) => new Order()
                {
                    StoreOwnerId = id,
                    CustomerId = currentUserId,
                    Items = items.Select(i => new OrderItem()
                    {
                        BookId = i.BookId,
                        Price = i.Book.Price,
                        Quantity = i.Quantity,
                    }).ToList(),
                });

            var orders = ordersQuery.ToList();
            context.AddRange(orders);
            context.RemoveRange(cartItems);

            await context.SaveChangesAsync();

            return Redirect("/");
        }
    }
}
