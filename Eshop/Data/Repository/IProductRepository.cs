using Eshop.Data.Models;
using Eshop.ViewModels.Account;
using Eshop.ViewModels.Product;

namespace Eshop.Data.Repository
{
    public interface IProductRepository
    {
        public void AddAsync(Product product);
        public void Update(Product product);
        public void Delete(int Id);
        public List<Product> GetAll();
        public Product GetById(int Id);
        public List<ListProductVm> GetAllWithIncludes();
        public void AddWithProductCategory(Product product, List<ProductCategory> pc);
        EditProductVm GetByIdForEdit(int id);
        void UpdateWithProductCategory(Product product, List<ProductCategory> pc);
        List<ListShoppingCardVm> GetByIds(List<int>? currentItems);
    }
}
