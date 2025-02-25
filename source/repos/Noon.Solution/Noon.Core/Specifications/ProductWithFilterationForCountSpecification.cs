using Noon.Core.Entities;

namespace Noon.Core.Specifications
{
    public class ProductWithFilterationForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterationForCountSpecification(ProductSpecParams productSpecParams)
            : base(P =>
                (string.IsNullOrEmpty(productSpecParams.Search) || P.Name.ToLower().Contains(productSpecParams.Search)) &&
                (!productSpecParams.BrandId.HasValue || P.ProductBrandId == productSpecParams.BrandId) &&
                (!productSpecParams.TypeId.HasValue || P.ProductTypeId == productSpecParams.TypeId)

                 )
        {

        }
    }
}
