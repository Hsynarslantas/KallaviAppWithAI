using Microsoft.AspNetCore.Mvc;

namespace YumApp.UI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
