using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Route("Main")]
    public class MainController : Controller
    {
        [Route("Index")]
        [Route("")]
        [Route("/")]
        public IActionResult Index()
        {
            var model = new BaseModel();
            model.TopMenu = new TopMenuModel { Number = 5 };
            model.Footer = new FooterModel { Number = 10 };

            return View(model);
        }
    }
}
