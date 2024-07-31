using AutoMapper;
using ToyStore.Api.DTOS;
using ToyStore.Core.Models;

namespace ToyStore.Api.Helpers
{
    public class AppMapper:Profile
    {
        public AppMapper()
        {
            CreateMap<AddCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<Product, ProductDto>().
                ForMember(dst=>dst.CategoryName , y=>y.MapFrom(x=>x.Category.Name))
                .ForMember(x=>x.ImageUrl , y=>y.MapFrom<ProductImageResolver>());
            CreateMap<AddProductDto, Product>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<CustomerBasketDto , Basket>();
            CreateMap<RegisterDto, AppUser>()
                .ForMember(x=>x.Role , y=>y.MapFrom(dst=>"User")); 

            CreateMap<AddAddressDto, Address>();
            CreateMap<Address, AddressDto>();
           
        }
    }
}
