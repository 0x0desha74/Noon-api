using Noon.Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noon.Core.Specifications
{
   public class OrderWithPaymentIntentIdSpecifications :BaseSpecification<Order>
    {
        public OrderWithPaymentIntentIdSpecifications(string paymentIntentId):base(order => order.PaymentIntentId == paymentIntentId)
        {

        }
    }
}
