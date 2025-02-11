using Noon.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noon.Core.Specifications
{
    public class ProductWithFilterationForCountSpecification: BaseSpecification<Product>
    {
        public ProductWithFilterationForCountSpecification(ProductSpecParams productSpecParams)
            :base(P =>
                (string.IsNullOrEmpty(productSpecParams.Search) || P.Name.ToLower().Contains(productSpecParams.Search))&& 
                (!productSpecParams.BrandId.HasValue || P.ProductBrandId == productSpecParams.BrandId)&&
                (!productSpecParams.TypeId.HasValue || P.ProductTypeId == productSpecParams.TypeId)
                
                 )
        {

        }
    }
}
