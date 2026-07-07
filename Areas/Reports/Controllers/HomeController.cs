using Microsoft.AspNetCore.Mvc;

namespace QuickPortals.Areas.Reports.Controllers
{
    [Area("Reports")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
