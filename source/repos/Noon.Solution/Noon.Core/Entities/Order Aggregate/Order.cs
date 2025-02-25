namespace Noon.Core.Entities.Order_Aggregate
{
    public class Order : BaseEntity
    {
        public Order() //For EF Core used during Migration
        {

        }

        public Order(string buyerEmail, ICollection<OrderItem> items, DeliveryMethod deliveryMethod, Address shippingAddress, decimal subTotal, string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            Items = items;
            DeliveryMethod = deliveryMethod;
            ShippingAddress = shippingAddress;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string BuyerEmail { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public DeliveryMethod DeliveryMethod { get; set; }
        public Address ShippingAddress { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Cost;
        public string PaymentIntentId { get; set; }
    }
}
