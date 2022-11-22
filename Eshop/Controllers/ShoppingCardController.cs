using Eshop.Data.Repository;
using Eshop.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace Eshop.Controllers
{
    public class ShoppingCardController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ShoppingCardController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IActionResult Index()
        {
            var basket = HttpContext.Session.GetString("basket");
            List<ListShoppingCardVm> result = new List<ListShoppingCardVm>();
            if (basket != null)
            {
                var currentItems = JsonSerializer.Deserialize<List<int>>(basket);
                result = _productRepository.GetByIds(currentItems);
            }
            return View(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToBasket(int id)
        {
            var basket = HttpContext.Session.GetString("basket");

            if (basket != null)
            {
                var currentItems = JsonSerializer.Deserialize<List<int>>(basket);
                currentItems.Add(id);
                HttpContext.Session.SetString("basket", JsonSerializer.Serialize(currentItems));
            }
            else
            {
                HttpContext.Session.SetString("basket", JsonSerializer.Serialize(new List<int> { id }));
            }



            TempData["Message"] = "با موفقیت به سبد افزوده شد";
            return RedirectToAction("index", "ShoppingCard");

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RemoveFromBasket(int id)
        {
            var basket = HttpContext.Session.GetString("basket");

            if (basket != null)
            {
                var basketProducts = JsonSerializer.Deserialize<List<int>>(basket);

                if (basketProducts.Where(x => x == id).Count()!=0)
                {
                    basketProducts.RemoveAll(x=>x==id);
                    HttpContext.Session.SetString("basket", JsonSerializer.Serialize(basketProducts));
                }
            }
            else
            {
                TempData["Message"] = "این رکورد در سبد وجود ندارد";
                return RedirectToAction("index", "ShoppingCard");
            }



            TempData["Message"] = "با موفقیت از سبد حذف شد";
            return RedirectToAction("index", "ShoppingCard");

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> RemoveItems()
        {
            HttpContext.Session.Remove("basket");

            TempData["Message"] = "سبد شما خالی است.";
            return RedirectToAction("all", "Product");

        }
    }
}
