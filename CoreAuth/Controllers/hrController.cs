using Microsoft.AspNetCore.Mvc;

namespace CoreAuth.Controllers
{
    public class hrController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
