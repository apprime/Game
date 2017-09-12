using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class TopMenuController : Controller
    {
        public IActionResult Index(TopMenuModel model)
        {
            return View();
        }
    }
}
