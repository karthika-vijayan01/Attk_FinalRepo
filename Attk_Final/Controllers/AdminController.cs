using Microsoft.AspNetCore.Mvc;

namespace Attk_Final.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
