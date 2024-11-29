using Microsoft.AspNetCore.Mvc;

namespace Attk_Final.Controllers
{
    public class DocController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
