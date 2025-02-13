using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noon.Core.Entities
{
    public class CustomerBasket
    {
        public string BasketId{ get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

        public CustomerBasket(string basketId)
        {
            BasketId = basketId;
        }
    }
}
