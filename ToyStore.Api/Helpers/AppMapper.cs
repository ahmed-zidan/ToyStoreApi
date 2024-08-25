using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ToyStore.Api.DTOS;
using ToyStore.Core.Models;
using ToyStore.Core.Models.Orders;

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
            CreateMap<ShipAddress, ShipAddressDto>();

            CreateMap<OrderItem, OrderItemToReturnDto>()
                .ForMember(x => x.ProductId, y => y.MapFrom(src => src.ProductItemOrder.ProductId))
                .ForMember(x => x.ProductName, y => y.MapFrom(src => src.ProductItemOrder.ProductName))
                .ForMember(x => x.PictureUrl, y => y.MapFrom<OrderImageResolver>());

            CreateMap<Order, OrderToReturnDto>();
            CreateMap<Category, CategoryDto>()
                .ForMember(x=>x.Image , y=>y.MapFrom<CategoryImageResolver>());

            CreateMap<Color, ListOFIdAndName>();
            CreateMap<Color, ColorDto>();
            CreateMap<Size, ListOFIdAndName>();
            CreateMap<IdentityRole, IdentityRoleDto>();
            CreateMap<Menu, MenuDto>();
            CreateMap<MenuAccess, MenuAccessDto>();
           
        }
    }
}
