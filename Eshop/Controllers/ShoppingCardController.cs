using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers
{
    public class ShoppingCardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
