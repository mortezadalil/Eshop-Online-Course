using Eshop.Data.Account;
using Eshop.ViewModels;
using Eshop.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static Dapper.SqlMapper;

namespace Eshop.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger _logger;

        public AccountController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            ILogger<AccountController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var result = _userManager.Users.Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .Select(x => new ListItemUserVm
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    UserName = x.UserName,
                    Roles = x.UserRoles.Select(y => y.Role.Name).ToList()
                }).ToList();
            return View(result);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.UserName);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "نام کاربری وجود ندارد");
                    return View();

                }
                //var passwordIsCorrect = await _userManager.CheckPasswordAsync(user, model.Password);
                //if (passwordIsCorrect)
                //{


                //}


                //اگر بخواهید همه کلیم های این کاربر را از دیتابیس پاک کنید
                await RemoveOldClaimsServer(user);

                //اگر بخواهید همه کلیم های این کاربر را از کلاینت مرورگر پاک کنید
                await RemoveOldClaimsClient();

                //اگر بخواهید ببینید یک کلیم خاص در دیتابیس برای این کاربر وجود دارد و اگر نبود اضافه کنید
                //این کلیم سمت سرور و کلاینت ثبت میشود
                var claims = await _userManager.GetClaimsAsync(user);
                if (claims.FirstOrDefault(x => x.Type == "phoneServer") == null)
                    await _userManager.AddClaimAsync(user, new Claim("phoneServer", user.PhoneNumber));

                //اگر بخواهید هر جایی یک کلیم را بخوانید و نمایش دهید
                var phone1 = (User.Claims).FirstOrDefault(c => c.Type == "phoneServer");//نال
                var phone2 = (User.Claims).FirstOrDefault(c => c.Type == "phoneClient");//نال


                // اضافه کردن کوکی در کلاینت و لاگین
                var clientCookie = (User as ClaimsPrincipal).Identity as ClaimsIdentity;
                clientCookie.AddClaim(new Claim("phoneClient", user.PhoneNumber));

                //اگر بخواهید هر جایی یک کلیم را بخوانید و نمایش دهید
                var phone1_ = (User.Claims).FirstOrDefault(c => c.Type == "phoneServer");//نال
                var phone2_ = (User.Claims).FirstOrDefault(c => c.Type == "phoneClient");//پر


                var passwordIsCorrect = await _userManager.CheckPasswordAsync(user, model.Password);
                if (passwordIsCorrect)
                    await _signInManager.SignInWithClaimsAsync(user, false, clientCookie.Claims);


                //اگر بخواهید هر جایی یک کلیم را بخوانید و نمایش دهید
                var phone1__ = (User.Claims).FirstOrDefault(c => c.Type == "phoneServer");//نال
                var phone2__ = (User.Claims).FirstOrDefault(c => c.Type == "phoneClient");//پر

                return RedirectToAction("Index", "Home");









                //لاگین بدون کوکی
                //var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false,
                //    lockoutOnFailure: false);
                //if (result.Succeeded)
                //{
                //    return RedirectToAction("Index", "Home");
                //}
                //else
                //    ModelState.AddModelError(string.Empty, "نام کاربری یا کلمه عبور اشتباه است");
            }
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.UserName, FirstName = model.FirstName, LastName = model.LastName };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }
        [HttpGet]
        public async Task<IActionResult> AccessProblem()
        {
            return Content("Access Denied");

        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewData["Roles"] = _roleManager.Roles.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id

            }).ToList(); ;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUserVm model)
        {
            ViewData["Roles"] = _roleManager.Roles.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id

            }).ToList();

            if (!ModelState.IsValid)
            {
                return View();
            }

            List<ApplicationUserRole> ur = new List<ApplicationUserRole>();
            foreach (var item in model.Roles)
            {
                if (item == "0") break;
                ur.Add(new ApplicationUserRole
                {
                    RoleId = item
                });
            }


            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserRoles = ur
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
            }


            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
                return View(model);

            }

            TempData["Message"] = "با موفقیت افزوده شد";
            return RedirectToAction("Index", "Account");

        }


        [HttpPost]
        public async Task<IActionResult> Delete(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            await _userManager.DeleteAsync(user);

            TempData["Message"] = "با موفقیت حذف شد";
            return RedirectToAction("Index", "Account");

        }



        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {


            var result = _userManager.Users
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .Where(x => x.Id == id)
                .Select(x => new EditUserVm
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    UserName = x.UserName,
                    Roles = x.UserRoles.Select(y => y.Role.Id).ToList()
                }).FirstOrDefault();

            ViewData["Roles"] = _roleManager.Roles.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id,
                Selected = result.Roles.Any(y => y == x.Name)

            }).ToList();

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserVm model)
        {


            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindByIdAsync(model.Id);


            user.UserName = model.UserName;
            user.Email = model.UserName;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;



            foreach (var roleId in model.Roles)
            {
                if (roleId == "0")
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, roles);
                    break;
                }

                var role = await _roleManager.FindByIdAsync(roleId);
                await _userManager.AddToRoleAsync(user, role.Name);
            }


            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                _logger.LogInformation("User updated a new account with password.");
            }


            foreach (var error in result.Errors)
            {
                TempData["Message"] = "مشکل در به روزرسانی";
                ModelState.AddModelError(string.Empty, error.Description);
                return View(model);
            }

            TempData["Message"] = "با موفقیت ویرایش شد";
            return RedirectToAction("Index", "Account");
        }


        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {


            var result = _userManager.Users
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .Where(x => x.Id == id)
                .Select(x => new DetailUserVm
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    UserName = x.UserName,
                    Roles = x.UserRoles.Select(y => y.Role.Id).ToList()
                }).FirstOrDefault();

            ViewData["Roles"] = _roleManager.Roles.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id,
                Selected = result.Roles.Any(y => y == x.Name)

            }).ToList();

            return View(result);
        }


        private async Task RemoveOldClaimsClient()
        {
            var user = User as ClaimsPrincipal;
            var identity = user.Identity as ClaimsIdentity;
            foreach (var item in User.Claims)
            {
                identity.RemoveClaim(item);
            }


        }

        private async Task RemoveOldClaimsServer(ApplicationUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);

            foreach (var item in claims)
            {
                await _userManager.RemoveClaimAsync(user, item);
            }

        }
    }
}
