using Eshop.Data.Models;
using Eshop.ViewModels.Account;
using Eshop.ViewModels.Product;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public OrderRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void Add(Order order)
        {
            _applicationDbContext.Add(order);
            _applicationDbContext.SaveChanges();
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public Order GetById(int id)
        {
            return _applicationDbContext.Order.FirstOrDefault(x=>x.Id==id);
        }

        public void Update(Order order)
        {
            _applicationDbContext.Order.Update(order);
            _applicationDbContext.SaveChanges();
        }
    }
}
