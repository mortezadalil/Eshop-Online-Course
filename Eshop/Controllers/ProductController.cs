using Eshop.Data.Account;
using Eshop.Data.Models;
using Eshop.Data.Repository;
using Eshop.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace Eshop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICompanyRepository _companyRepository;

        public ProductController(IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            ICompanyRepository companyRepository
            )
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _companyRepository = companyRepository;
        }
        public IActionResult index()
        {
            var product = _productRepository.GetAllWithIncludes();
            //به لیست کالا و کتگوریها و کمپانی هر کدام نیاز داریم.
            return View(product);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Add()
        {
            ViewData["Categories"] = _categoryRepository.GetAll().ToList().Select(x => new SelectListItem
            {
                Text = x.Title, 
                Value = x.Id.ToString()

            }).ToList(); ;

            ViewData["Companies"] = _companyRepository.GetAll().ToList().Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.Id.ToString()

            }).ToList(); ;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Add(AddProductVm model)
        {
            ViewData["Categories"] = _categoryRepository.GetAll().ToList().Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.Id.ToString()

            }).ToList(); ;

            ViewData["Companies"] = _companyRepository.GetAll().ToList().Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.Id.ToString()

            }).ToList();


            if (!ModelState.IsValid)
            {
                return View();
            }


            try
            {
                var product = new Product
                {
                    Price = model.Price,
                    CreatedDate = DateTime.Now,
                    Description = model.Description,
                    Title = model.Title,
                    ShortDescription = model.ShortDescription,
                    CompanyId = model.CompanyId
                };

                List<ProductCategory> pc = new List<ProductCategory>();
                foreach (var catId in model.CategoryIds)
                {
                    if (catId == 0) break;
                    var productCategorory = new ProductCategory
                    {
                        CategoryId = catId,
                        Product = product
                    };

                    pc.Add(productCategorory);
                }

                _productRepository.AddWithProductCategory(product, pc);

                TempData["Message"] = "با موفقیت افزوده شد";
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;

            }



            return RedirectToAction("Index", "Product");

        }





        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Categories"] = _categoryRepository.GetAll().ToList().Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.Id.ToString()

            }).ToList(); ;

            ViewData["Companies"] = _companyRepository.GetAll().ToList().Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.Id.ToString()

            }).ToList();

            var result = _productRepository.GetByIdForEdit(id);

            return View(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(EditProductVm model)
        {

            ViewData["Categories"] = _categoryRepository.GetAll().ToList().Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.Id.ToString()

            }).ToList(); ;

            ViewData["Companies"] = _companyRepository.GetAll().ToList().Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.Id.ToString()

            }).ToList();


            if (!ModelState.IsValid)
            {
                return View();
            }


            try
            {
                var product = _productRepository.GetById(model.Id);
                product.Description = model.Description;
                product.CreatedDate = DateTime.Now;
                product.ShortDescription = model.ShortDescription;
                product.CompanyId = model.CompanyId;
                product.Title = model.Title;


                List<ProductCategory> pc = new List<ProductCategory>();
                foreach (var catId in model.CategoryIds)
                {
                    if (catId == 0) break;
                    var productCategorory = new ProductCategory
                    {
                        CategoryId = catId,
                        Product = product
                    };

                    pc.Add(productCategorory);
                }

                product.ProductCategories = pc;

                _productRepository.UpdateWithProductCategory(product, pc);

                TempData["Message"] = "با موفقیت ویرایش شد";
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;

            }



            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Detail(int id)
        {


            ViewData["Categories"] = _categoryRepository.GetAll().ToList().Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.Id.ToString()

            }).ToList(); ;

            ViewData["Companies"] = _companyRepository.GetAll().ToList().Select(x => new SelectListItem
            {
                Text = x.Title,
                Value = x.Id.ToString()

            }).ToList();

            var result = _productRepository.GetByIdForEdit(id);


            return View(result);
        }

        public IActionResult all()
        {
            var products = _productRepository.GetAllWithIncludes();
            //به لیست کالا و کتگوریها و کمپانی هر کدام نیاز داریم.
            return View(products);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            _productRepository.Delete(Id);

            TempData["Message"] = "با موفقیت حذف شد";
            return RedirectToAction("Index", "Product");

        }

      
    }
}
