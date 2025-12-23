using Microsoft.AspNetCore.Mvc;

namespace YumApp.UI.ViewComponents.Default
{
    public class _DefaultReservationComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
