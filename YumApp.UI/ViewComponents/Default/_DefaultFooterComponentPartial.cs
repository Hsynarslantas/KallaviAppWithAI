using Microsoft.AspNetCore.Mvc;

namespace YumApp.UI.ViewComponents.Default
{
    public class _DefaultFooterComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
