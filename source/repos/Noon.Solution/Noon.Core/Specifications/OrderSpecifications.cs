using Noon.Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noon.Core.Specifications
{
    public class OrderSpecifications : BaseSpecification<Order>
    {
        public OrderSpecifications(string email) : base(order => order.BuyerEmail == email)
        {
            Includes.Add(Order => Order.DeliveryMethod);
            Includes.Add(Order => Order.ShippingAddress);
            Includes.Add(Order => Order.Items);
            
            AddOrderDescBy(order => order.OrderDate);
        }

        public OrderSpecifications(string email, int id):base(order=>order.BuyerEmail ==email && order.Id == id)
        {
            Includes.Add(Order => Order.DeliveryMethod);
            Includes.Add(Order => Order.ShippingAddress);
            Includes.Add(Order => Order.Items);

        }
    }
}
