using AutoMapper;
using ToyStore.Api.DTOS;
using ToyStore.Core.Models.Orders;

namespace ToyStore.Api.Helpers
{
    public class OrderImageResolver : IValueResolver<OrderItem, OrderItemToReturnDto, string>
    {
        private readonly IConfiguration _configuration;
        public OrderImageResolver(IConfiguration configuration)
        {
                _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (source.ProductItemOrder.PictureUrl == null) {
                return _configuration["ApiUrl"] + "Resources/Images/Default.png";
            }
            else
            {
                return _configuration["ApiUrl"] + source.ProductItemOrder.PictureUrl;
            }
        }
    }
}
