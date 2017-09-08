using Microsoft.AspNetCore.Mvc;
namespace eSportMK.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
