using Microsoft.AspNetCore.Mvc;

namespace YumApp.UI.Controllers
{
    public class AdminDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
