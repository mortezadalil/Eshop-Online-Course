using Eshop.Data.Models;
using Eshop.ViewModels.Product;

namespace Eshop.Data.Repository
{
    public interface ICompanyRepository
    {
        public void AddAsync(Company product);
        public void Update(Company product);
        public void Delete(int Id);
        public List<Company> GetAll();
        public Company GetById(int Id);
    }
}
