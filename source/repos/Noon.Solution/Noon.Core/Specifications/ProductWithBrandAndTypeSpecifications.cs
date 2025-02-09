using Noon.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Noon.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecification<Product>
    {
        public ProductWithBrandAndTypeSpecifications(int id) : base(P => P.Id == id)
        {
            Includes.Add(P => P.Type);
            Includes.Add(P => P.Brand);
        }
     

        public ProductWithBrandAndTypeSpecifications(string? sort,int? brandId, int? typeId) : base(
             P => (!brandId.HasValue || P.Brand.Id == brandId) &&
                  (!typeId.HasValue || P.Type.Id == typeId)
            )
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderAscBy(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderDescBy(P => P.Price);
                        break;
                    default:
                        AddOrderAscBy(P => P.Name);
                        break;
                }
            }

            Includes.Add(P => P.Type);
            Includes.Add(P => P.Brand);
        }
    }
}
