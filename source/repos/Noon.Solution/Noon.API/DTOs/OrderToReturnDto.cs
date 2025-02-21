using Noon.Core.Entities.Order_Aggregate;

namespace Noon.API.DTOs
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public string Status { get; set; }
        public DateTimeOffset OrderDate { get; set; } 
        public ICollection<OrderItemDto> Items { get; set; } 
        public string  DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }
        public Address ShippingAddress { get; set; }
        public decimal Total { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
