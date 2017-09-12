using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class MainController : Controller
    {
        public IActionResult TopMenu()
        {
            return View();
        }
    }
}
