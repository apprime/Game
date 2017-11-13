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
        [HttpGet]
        public IActionResult Index()
        {
            var model = new Credentials
            {
                LoginFailed = TempData.ContainsKey("LoginFailed") ? (bool)TempData["LoginFailed"] : false,
                TopMenu = new TopMenuModel { Number = 5 },
                Footer = new FooterModel { Number = 10 }
            };

            return View(model);
        }

        [Route("Game")]
        public IActionResult Game()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Credentials credentials)
        {
            if(credentials.Email == "mctest@email.com" && credentials.Password == "password123!")
            {
                //Todo: Set up session here and redirect to index
                return RedirectToAction("Game");
            }

            if (!TempData.ContainsKey("Login"))
            {
                TempData.Add("LoginFailed", true);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(User user)
        {
            return View();
        }
    }
}
