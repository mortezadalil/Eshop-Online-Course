using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult index()
        {
            return View();
        }
        public IActionResult all()
        {
            return View();
        }
    }
}
