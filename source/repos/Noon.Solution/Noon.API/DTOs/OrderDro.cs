using Noon.Core.Entities.Order_Aggregate;

namespace Noon.API.DTOs
{
    public class OrderDro
    {
        public string BasketId { get; set; }
        public AddressDto ShippingAddress { get; set; }
        public int DeliveryMethodId{ get; set; }
    }
}
