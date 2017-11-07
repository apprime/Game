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
            var model = new Credentials();
            model.TopMenu = new TopMenuModel { Number = 5 };
            model.Footer = new FooterModel { Number = 10 };

            return View(model);
        }

        [Route("Game")]
        public IActionResult Game()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(Credentials credentials)
        {
            if(credentials.Email == "mctest@email.com" && credentials.Password == "password123!")
            {
                return RedirectToAction("Game");
            }
            else
            {
                ModelState.AddModelError("", "Authentication Error");
                return new EmptyResult();
            }
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
