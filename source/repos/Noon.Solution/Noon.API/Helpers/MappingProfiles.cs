using AutoMapper;
using Noon.API.DTOs;
using Noon.Core.Entities;
using Noon.Core.Entities.Order_Aggregate;

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

            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, Address>();

            //OrderToReturn 

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.Status, O => O.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.DeliveryMethod, O => O.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost, O => O.MapFrom(s => s.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductName, O => O.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, O => O.MapFrom(s => s.Product.PictureUrl))
                .ForMember(d => d.ProductId, O => O.MapFrom(s => s.Product.ProductId))
                /*.ForMember(d=>d.ProductUrl, O=>O.MapFrom<>())*/;

        }

    }
}
