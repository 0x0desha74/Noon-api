namespace Noon.Core.Entities
{
    public class CustomerBasket
    {

        public string Id { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }
        public decimal ShippingCost { get; set; }
        public int? DeliveryMethodId { get; set; }
        public CustomerBasket(string id)
        {
            Id = id;
        }

    }
}
