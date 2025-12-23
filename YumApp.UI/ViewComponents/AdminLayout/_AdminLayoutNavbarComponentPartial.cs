using Microsoft.AspNetCore.Mvc;

namespace YumApp.UI.ViewComponents.AdminLayout
{
    public class _AdminLayoutNavbarComponentPartial:ViewComponent
    {
        
        public IViewComponentResult Invoke()
        {
           
            return View();
        }
    }
}
