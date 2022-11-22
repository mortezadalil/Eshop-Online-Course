using Eshop.Data.Models;
using Eshop.ViewModels.Account;
using Eshop.ViewModels.Product;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void AddAsync(Product product)
        {
            _applicationDbContext.Product.Add(product);
            _applicationDbContext.SaveChanges();
        }

        public void AddWithProductCategory(Product product, List<ProductCategory> pc)
        {
            _applicationDbContext.Product.Add(product);
            _applicationDbContext.ProductCategory.AddRange(pc);
            _applicationDbContext.SaveChanges();

        }

        public void Delete(int Id)
        {
            _applicationDbContext.Product.Remove(GetById(Id));
            _applicationDbContext.SaveChanges();
        }

        public List<Product> GetAll()
        {
            return _applicationDbContext.Product.ToList();
        }

        public List<ListProductVm> GetAllWithIncludes()
        {
            var result = _applicationDbContext.Product
                .Include(x => x.ProductCategories)
                .ThenInclude(x => x.Category)
                .Include(x => x.Company)
                .Select(x => new ListProductVm
                {
                    Id = x.Id,
                    Categories = x.ProductCategories.Select(x => x.Category).ToList(),
                    Company = x.Company,
                    Price = x.Price,
                    CreatedDate = x.CreatedDate,
                    ShortDescription = x.ShortDescription,
                    Title = x.Title
                }).ToList();

            return result;
        }

        public Product GetById(int Id)
        {
            return _applicationDbContext.Product.FirstOrDefault(x => x.Id == Id);
        }

        public EditProductVm GetByIdForEdit(int id)
        {
            return _applicationDbContext.Product
                .Include(x => x.ProductCategories)
                .ThenInclude(x => x.Category)
                .Select(x => new EditProductVm
                {
                    Id = x.Id,
                    CategoryIds = x.ProductCategories.Select(x => x.CategoryId).ToList(),
                    CompanyId = x.CompanyId,
                    Price = x.Price,
                    ShortDescription = x.ShortDescription,
                    Title = x.Title,
                    Description = x.Description
                }).FirstOrDefault(x => x.Id == id);
        }

        public List<ListShoppingCardVm> GetByIds(List<int>? currentItems)
        {
            return _applicationDbContext.Product.Where(x => currentItems.Contains(x.Id))
                .ToList()
                .Select(x => new ListShoppingCardVm
                {
                    Id = x.Id,
                    Price = x.Price,
                    Title = x.Title,
                    Count = currentItems.Where(y => y == x.Id).Count()
                })
                .ToList();
        }

        public void Update(Product product)
        {
            _applicationDbContext.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _applicationDbContext.SaveChanges();

        }

        public void UpdateWithProductCategory(Product product, List<ProductCategory> pc)
        {
            //Remove old productCategories

            var oldProductCategories = _applicationDbContext.
                ProductCategory.
                Where(x => x.ProductId == product.Id).ToList();
            _applicationDbContext.ProductCategory.RemoveRange(oldProductCategories);
            _applicationDbContext.Update(product);
            _applicationDbContext.SaveChanges();
        }
    }
}
