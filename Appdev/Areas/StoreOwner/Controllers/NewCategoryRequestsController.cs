using AppDev.Data;
using AppDev.Models;
using AppDev.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppDev.Areas.StoreOwner.Controllers
{
    [Area("StoreOwner")]
    [Authorize(Roles = Role.StoreOwner)]
    public class NewCategoryRequestsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public NewCategoryRequestsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        private string GetCurrentUserId()
        {
            return userManager.GetUserId(User);
        }
        // GET: StoreOwner/Categories
        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();

            var requests = await context.NewCategoryRequests
                .Where(x => x.StoreOwnerId == userId).
                ToListAsync();

            return View(requests);
        }

        // GET: StoreOwner/Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StoreOwner/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewCategoryRequestCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await context.Categories.AnyAsync(c => c.Name == model.Name))
                {
                    ModelState.AddModelError("", "Category is already existed.");
                    return View(model);
                }
                if (await context.NewCategoryRequests.AnyAsync(c => c.Name == model.Name))
                {
                    ModelState.AddModelError("", "Request for this category is already existed.");
                    return View(model);
                }

                var request = new NewCategoryRequest()
                {
                    Name = model.Name,
                    StoreOwnerId = GetCurrentUserId(),
                };

                context.Add(request);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}
