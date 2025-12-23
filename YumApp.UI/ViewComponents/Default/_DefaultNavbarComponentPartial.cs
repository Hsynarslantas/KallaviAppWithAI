using Microsoft.AspNetCore.Mvc;

namespace YumApp.UI.ViewComponents.Default
{
    public class _DefaultNavbarComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
