using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class StyleguideController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TopMenu()
        {
            return View();
        }

        public IActionResult Footer()
        {
            return View();
        }
    }
}
