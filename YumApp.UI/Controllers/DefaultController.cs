using Microsoft.AspNetCore.Mvc;

namespace YumApp.UI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
