using Eshop.Data.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleController(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public IActionResult index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddTestRole()
        {
            IdentityResult result = await _roleManager.CreateAsync(new ApplicationRole() { Name = "Admin" });

            return View();
        }
    }
}
