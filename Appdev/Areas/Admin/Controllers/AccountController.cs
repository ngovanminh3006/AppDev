using AppDev.Areas.Admin.ViewModels;
using AppDev.Data;
using AppDev.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppDev.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Role.Admin)]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> StoreOwner()
        {
            var users = await userManager.GetUsersInRoleAsync(Role.StoreOwner);
            return View("Users", users);
        }

        public async Task<IActionResult> Customer()
        {
            var users = await userManager.GetUsersInRoleAsync(Role.Customer);
            return View("Users", users);
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string? id)
        {
            if (id == null)
                return View();

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var model = new ResetPasswordViewModel()
            {
                Email = user.Email,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "Account does not exist.");
                    return View(model);
                }

                if (await userManager.IsInRoleAsync(user, Role.Admin))
                {
                    ModelState.AddModelError("", "Cannot reset password of admin account");
                    return View(model);
                }

                var resetCode = await userManager.GeneratePasswordResetTokenAsync(user);

                await userManager.ResetPasswordAsync(user, resetCode, model.Password);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}
