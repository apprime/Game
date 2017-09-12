using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class FooterViewComponent : ViewComponent
    {
        //Todo: Consider async components. Then, after considering, implement it.
        public IViewComponentResult Invoke()
        {
            var model = new FooterModel { Number = 3 }; //TODO: Footer should contain stuff from service
            return View(model);
        }
    }
}
