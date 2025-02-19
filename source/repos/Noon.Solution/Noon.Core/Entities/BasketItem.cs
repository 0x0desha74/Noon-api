﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noon.Core.Entities
{
    public class BasketItem 
    {

        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal  Price { get; set; }
        public string PictureUrl{ get; set; }
        public int Quantity{ get; set; }
        public string brand{ get; set; }
        public string type{ get; set; }
    }
}
