using Microsoft.AspNetCore.Mvc;

namespace AppDev.Controllers
{
    public class Help : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
