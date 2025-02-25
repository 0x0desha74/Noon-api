using Noon.Core;
using Noon.Core.Entities;
using Noon.Core.Entities.Order_Aggregate;
using Noon.Core.Repositories;
using Noon.Core.Services;
using Noon.Core.Specifications;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;


        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork, IPaymentService paymentService)
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
        }

        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, Address shippingAddress, int deliveryMethodId)
        {

            var basket = await _basketRepo.GetBasketAsync(basketId);

            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetById(item.Id);
                var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);
                items.Add(orderItem);

            }
            var subtotal = items.Sum(product => (product.Price * product.Quantity));
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetById(deliveryMethodId);

            var spec = new OrderWithPaymentIntentIdSpecifications(basket.PaymentIntentId);
            var existingOrder = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
            if(existingOrder is not null)
            {
                 _unitOfWork.Repository<Order>().Delete(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(basket.Id);
            }

            var order = new Order(buyerEmail, items, deliveryMethod, shippingAddress, subtotal, basket.PaymentIntentId);
            await _unitOfWork.Repository<Order>().AddAsync(order);
            var result = await _unitOfWork.Complete();

            return result > 0 ? order : null;

        }

        public Task<Order> GetOrderByIdForUserAsync(string buyerEmail, int orderId)
        {
            var spec = new OrderSpecifications(buyerEmail, orderId);
            var order = _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
            return order is null ? null : order;
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecifications(buyerEmail);
            var orders = _unitOfWork.Repository<Order>().GetAllWithSpec(spec);
            return orders;
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
              => _unitOfWork.Repository<DeliveryMethod>().GetAll();


    }
}
