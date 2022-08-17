using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
