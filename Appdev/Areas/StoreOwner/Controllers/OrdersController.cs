using AppDev.Data;
using AppDev.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppDev.Areas.StoreOwner.Controllers
{
    [Area("StoreOwner")]
    [Authorize(Roles = Role.StoreOwner)]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        private string GetCurrentOwnerId()
        {
            return userManager.GetUserId(User);
        }
        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var currentUserId = GetCurrentOwnerId();

            var orders = await context.Orders
                .Include(o => o.Customer)
                .Where(o => o.StoreOwnerId == currentUserId)
                .ToListAsync();
            return View(orders);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || context.Orders == null)
            {
                return NotFound();
            }

            var order = await context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Items)
                .ThenInclude(i => i.Book)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}
