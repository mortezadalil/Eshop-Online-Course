using Eshop.Data.Models;
using Eshop.ViewModels.Product;

namespace Eshop.Data.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CategoryRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void AddAsync(Category product)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAll()
        {
            return _applicationDbContext.Category.ToList();
        }

        public Category GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public void Update(Category product)
        {
            throw new NotImplementedException();
        }
    }
}
