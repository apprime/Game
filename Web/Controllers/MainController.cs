using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Route("Main")]
    public class MainController : Controller
    {
        [Route("Index")]
        [Route("")]
        public IActionResult Index()
        {
            var model = new BaseModel();
            model.TopMenu = new TopMenuModel { Number = 5 };

            return View(model);
        }
    }
}
