using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("Styleguide")]
    public class StyleguideController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("TopMenu")]
        public IActionResult TopMenu()
        {
            return View();
        }

        [Route("Footer")]
        public IActionResult Footer()
        {
            return View();
        }

        [Route("Interface")]
        public IActionResult Interface()
        {
            return View();
        }
    }
}
