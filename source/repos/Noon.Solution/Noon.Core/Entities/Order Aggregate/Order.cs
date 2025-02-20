using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noon.Core.Entities.Order_Aggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {

        }
        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; set; } //Not a Navigational Property
        public DeliveryMethod DeliveryMethod { get; set; } //Navigational Property [One]
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; } //SunTotal = ProductPrice * Quantity
        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Cost;
        public string PaymentIntentId { get; set; }
    }
}
