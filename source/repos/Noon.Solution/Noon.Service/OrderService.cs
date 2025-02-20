using Noon.Core.Entities;
using Noon.Core.Entities.Order_Aggregate;
using Noon.Core.Repositories;
using Noon.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noon.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepo;
        private readonly IGenericRepository<Order> _orderRepo;


        public OrderService(IBasketRepository basketRepo,
            IGenericRepository<Product> productRepo,
            IGenericRepository<DeliveryMethod> deliveryMethodRepo,
            IGenericRepository<Order> orderRepo)
        {
            _basketRepo = basketRepo;
            _productRepo = productRepo;
            _deliveryMethodRepo = deliveryMethodRepo;
            _orderRepo = orderRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, Address shippingAddress, int deliveryMethodId)
        {

            var basket = await _basketRepo.GetBasketAsync(basketId);

            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await _productRepo.GetById(item.Id);
                var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                var orderItem = new OrderItem(productItemOrdered, item.Quantity, product.Price);
                items.Add(orderItem);

            }
            var subtotal = items.Sum(product => (product.Price * product.Quantity));
            var deliveryMethod = await _deliveryMethodRepo.GetById(deliveryMethodId);
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, items, subtotal);
            await _orderRepo.AddAsync(order);
            return order;

        }

        public Task<Order> GetOrderByIdForUserAsync(string buyerEmail, int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
