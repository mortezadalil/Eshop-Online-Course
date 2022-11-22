using Eshop.Data.Models;
using Eshop.ViewModels.Product;

namespace Eshop.Data.Repository
{
    public interface ICategoryRepository
    {
        public void AddAsync(Category product);
        public void Update(Category product);
        public void Delete(int Id);
        public List<Category> GetAll();
        public Category GetById(int Id);
    }
}
