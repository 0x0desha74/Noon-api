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


        public ProductWithBrandAndTypeSpecifications(ProductSpecParams ProductSpecParams)
            : base(P =>

                    (string.IsNullOrEmpty(ProductSpecParams.Search) || P.Name.ToLower().Contains(ProductSpecParams.Search)) &&
                    (!ProductSpecParams.BrandId.HasValue || P.Brand.Id == ProductSpecParams.BrandId) &&
                    (!ProductSpecParams.BrandId.HasValue || P.Type.Id == ProductSpecParams.BrandId)
            )
        {




            ApplyPagination((ProductSpecParams.PageIndex -1) * ProductSpecParams.PageSize ,ProductSpecParams.PageSize);

            if (!string.IsNullOrEmpty(ProductSpecParams.Sort))
            {
                switch (ProductSpecParams.Sort)
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
