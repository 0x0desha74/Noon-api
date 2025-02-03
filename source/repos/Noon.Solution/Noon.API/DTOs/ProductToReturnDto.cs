using Noon.Core.Entities;

namespace Noon.API.DTOs
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int ProductTypeId { get; set; }
        public string Type { get; set; }
        public int ProductBrandId { get; set; }
        public string Brand { get; set; }
    }
}
