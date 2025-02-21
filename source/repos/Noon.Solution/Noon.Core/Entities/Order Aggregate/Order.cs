using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noon.Core.Entities.Order_Aggregate
{
    public class Order: BaseEntity
    {
        public Order() //For EF Core used during Migration
        {

        }

        public Order(string buyerEmail, ICollection<OrderItem> items, DeliveryMethod deliveryMethod, Address shippingAddress, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            Items = items;
            DeliveryMethod = deliveryMethod;
            ShippingAddress = shippingAddress;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public DeliveryMethod DeliveryMethod{ get; set; }
        public Address ShippingAddress { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Cost;
        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
