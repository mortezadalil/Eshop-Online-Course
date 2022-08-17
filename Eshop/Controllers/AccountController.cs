using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
