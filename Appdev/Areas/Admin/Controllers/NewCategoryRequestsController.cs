using AppDev.Areas.Admin.ViewModels;
using AppDev.Data;
using AppDev.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppDev.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Role.Admin)]
    public class NewCategoryRequestsController : Controller
    {
        private readonly ApplicationDbContext context;

        public NewCategoryRequestsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: Admin/CategoryRequests
        public async Task<IActionResult> Index()
        {
            var requests = await context.NewCategoryRequests.Include(n => n.StoreOwner).ToListAsync();
            return View(requests);
        }

        // GET: Admin/CategoryRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || context.NewCategoryRequests == null)
            {
                return NotFound();
            }

            var newCategoryRequest = await context.NewCategoryRequests
                .Include(n => n.StoreOwner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newCategoryRequest == null)
            {
                return NotFound();
            }

            return View(newCategoryRequest);
        }

        public async Task<IActionResult> Approval(int? id)
        {
            if (id == null)
                return NotFound();

            var request = await context.NewCategoryRequests
                .Include(r => r.StoreOwner)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (request == null)
                return NotFound();

            if (request.IsApproval == true)
            {
                return RedirectToAction(nameof(Index));
            }

            var model = new NewCategoryRequestApprovalViewModel()
            {
                Id = request.Id,
                Name = request.Name,
                StoreOwnerId = request.StoreOwnerId,
                StoreOwnerFullName = request.StoreOwner.FullName,
                IsApproval = request.IsApproval,
                Message = request.Message!,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approval(int? id, NewCategoryRequestApprovalViewModel model)
        {
            if (id == null || id != model.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var request = await context.NewCategoryRequests.FirstOrDefaultAsync(r => r.Id == model.Id);

                if (request == null)
                    return NotFound();

                if (request.IsApproval != null)
                {
                    return RedirectToAction(nameof(Index));
                }

                request.Id = model.Id;
                request.Name = model.Name;
                request.StoreOwnerId = model.StoreOwnerId;
                request.Message = model.Message;
                request.IsApproval = model.IsApproval;

                context.Update(request);

                if (model.IsApproval == true)
                {
                    var category = new Category()
                    {
                        Name = model.Name,
                        IsActive = true
                    };

                    context.Add(category);
                }

                try
                {
                    await context.SaveChangesAsync();

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.InnerException?.Message ?? ex.Message);
                    return View(model);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool NewCategoryRequestExists(int id)
        {
            return (context.NewCategoryRequests?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
