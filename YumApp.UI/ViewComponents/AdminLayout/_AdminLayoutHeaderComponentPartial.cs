using Microsoft.AspNetCore.Mvc;

namespace YumApp.UI.ViewComponents.AdminLayout
{
    public class _AdminLayoutHeaderComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
