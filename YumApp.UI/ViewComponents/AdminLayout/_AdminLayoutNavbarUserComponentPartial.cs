using Microsoft.AspNetCore.Mvc;

namespace YumApp.UI.ViewComponents.AdminLayout
{
    public class _AdminLayoutNavbarUserComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
