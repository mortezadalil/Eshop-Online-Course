using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult index()
        {
            return View();
        }
    }
}
