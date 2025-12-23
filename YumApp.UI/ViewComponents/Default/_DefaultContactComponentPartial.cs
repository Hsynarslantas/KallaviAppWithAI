using Microsoft.AspNetCore.Mvc;

namespace YumApp.UI.ViewComponents.Default
{
    public class _DefaultContactComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
