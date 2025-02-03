using Noon.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noon.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecification<Product> 
    {
        public ProductWithBrandAndTypeSpecifications(int id):base(P =>P.Id == id)
        {
            Includes.Add(P => P.Type);
            Includes.Add(P => P.Brand);
        }


        public ProductWithBrandAndTypeSpecifications()
        {
            Includes.Add(P => P.Type);
            Includes.Add(P => P.Brand);
        }
    }
}
