using Eshop.Data.Account;
using Eshop.Extensions;
using Eshop.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Eshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }
        [ServiceFilter(typeof(DemoFilter))]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Inside Action");
                   //اگر بخواهید هر جایی یک کلیم را بخوانید و نمایش دهید
                   var phone1 = (User.Claims).FirstOrDefault(c => c.Type == "phoneServer");//پر
            var phone2 = (User.Claims).FirstOrDefault(c => c.Type == "phoneClient");//پر

            int i = 10;
            bool result = i.IsGreaterThan(100);

            var persian = DateTime.Now.ToPersianDate();


            throw new Exception("ERROR");
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}