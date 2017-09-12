using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class TopMenuViewComponent : ViewComponent
    {
        //Todo: Consider async components. Then, after considering, implement it.
        public IViewComponentResult Invoke()
        {
            var model = new TopMenuModel { Number = 3 }; //TODO: TopMenu should contain user data for the "Logged in" widget. Fetch from some service.
            return View(model);
        }
    }
}
