using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        [Route("Index")]
        [Route("")]
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("About")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Route("Contact")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [Route("Error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
