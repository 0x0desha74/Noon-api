using AutoMapper;
using Microsoft.Extensions.Options;
using Noon.API.DTOs;
using Noon.Core.Entities;

namespace Noon.API.Helpers
{
    public class ProductPictureResolver : IValueResolver<Product, ProductToReturnDto, string>
    {

        private readonly IConfiguration _configuration;

        public ProductPictureResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.Name))
            {
                return $"{_configuration["ApiBaseUrl"]}{source.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
