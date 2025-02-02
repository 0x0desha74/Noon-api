using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noon.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType Type  { get; set; }
        public int ProductBrandId { get; set; }
        public ProductBrand Brand { get; set; }
    }
}
