using Eshop.Data.Models;
using Eshop.ViewModels.Account;
using Eshop.ViewModels.Product;

namespace Eshop.Data.Repository
{
    public interface IOrderRepository
    {
        public void Add(Order order);
        public void Update(Order order);
        public void Delete(int Id);
        public List<Order> GetAll();
        public Order GetById(int Id);
    }
}
