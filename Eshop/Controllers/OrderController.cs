using Eshop.Data.Models;
using Eshop.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace Eshop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderController(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }
        public IActionResult index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Payment()
        {
            var basket = HttpContext.Session.GetString("basket");
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            if (basket != null)
            {
                var basketProducts = JsonSerializer.Deserialize<List<int>>(basket);

                foreach (var item in basketProducts)
                {
                    var product = _productRepository.GetById(item);

                    var od = orderDetails.FirstOrDefault(x => x.ProductId == item);
                    if (od == null)
                    {
                        orderDetails.Add(new OrderDetail
                        {
                            Count = 0,
                            Price = product.Price ?? 0,
                            ProductId = product.Id
                        });

                    }
                    else
                    {
                        od.Count = od.Count + 1;
                    }
                }


                var order = new Order
                {
                    CreatedDate = DateTime.Now,
                    OrderDetails = orderDetails,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    Status = OrderStatus.Created
                };

                _orderRepository.Add(order);

                return RedirectToActionPermanent("FakeBank", new { id = order.Id });
            }
            return View();
        }

        public IActionResult FakeBank(int id)
        {
            return RedirectToActionPermanent("CallBack", new { id = id, status = 1 });
        }


        public IActionResult CallBack(int id, int status)
        {
            var order = _orderRepository.GetById(id);
            switch (status)
            {
                case 1:
                    order.Status = OrderStatus.Success;
                    break;
                case -1:
                    order.Status = OrderStatus.Failed;
                
                    break;
                default:
                    break;
            }

            _orderRepository.Update(order);
            return View(order.Status);
        }
    }
}
