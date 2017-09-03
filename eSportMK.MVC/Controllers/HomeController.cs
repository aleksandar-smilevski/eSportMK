using Microsoft.AspNetCore.Mvc;
namespace eSportMK.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
