using Microsoft.AspNetCore.Mvc;

namespace YumApp.UI.ViewComponents.Default.DefaultMenu
{
    public class _DefaultMenuComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
