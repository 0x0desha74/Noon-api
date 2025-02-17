using AutoMapper;
using Noon.API.DTOs;
using Noon.Core.Entities;
using Noon.Core.Entities.Identity;

namespace Noon.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Brand, O => O.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.Type, O => O.MapFrom(s => s.Type.Name))
                .ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureResolver>());

            CreateMap<Address, AddressDto>();
        }

    }
}
