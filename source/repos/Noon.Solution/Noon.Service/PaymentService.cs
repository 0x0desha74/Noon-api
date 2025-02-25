using Microsoft.Extensions.Configuration;
using Noon.Core;
using Noon.Core.Entities;
using Noon.Core.Entities.Order_Aggregate;
using Noon.Core.Repositories;
using Noon.Core.Services;
using Stripe;
using Product = Noon.Core.Entities.Product;


namespace Noon.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _config;
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration config, IBasketRepository basketRepo, IUnitOfWork unitOfWork)
        {
            _config = config;
            this._basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _config["StripeKeys:SecretKey"];

            var basket = await _basketRepo.GetBasketAsync(basketId);


            if (basket is null) return null;
            var shippingCost = 0m;

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetById(basket.DeliveryMethodId.Value);
                basket.ShippingCost = deliveryMethod.Cost;
                shippingCost = deliveryMethod.Cost;
            }

            if (basket?.Items?.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetById(item.Id);
                    if (item.Price != product.Price)
                        item.Price = product.Price;
                }
            }

            var services = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = CalculateTotalAmountOfOrder(basket, shippingCost),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" }
                };
                paymentIntent = await services.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = CalculateTotalAmountOfOrder(basket, shippingCost)
                };
                paymentIntent = await services.UpdateAsync(basket.PaymentIntentId, options);
            }

            await _basketRepo.UpdateBasketAsync(basket);

            return basket;

        }



        private long CalculateTotalAmountOfOrder(CustomerBasket basket, decimal shippingCost)
        => (long)basket.Items.Sum(item => item.Quantity * item.Price * 100) + (long)shippingCost * 100;

    }
}
