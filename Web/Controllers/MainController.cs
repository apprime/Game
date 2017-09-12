using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            var model = new BaseModel();
            model.TopMenu = new TopMenuModel { Number = 5 };

            return View(model);
        }
    }
}
