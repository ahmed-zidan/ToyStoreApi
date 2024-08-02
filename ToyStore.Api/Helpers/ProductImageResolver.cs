using AutoMapper;
using AutoMapper.Execution;
using ToyStore.Api.DTOS;
using ToyStore.Core.Models;

namespace ToyStore.Api.Helpers
{
    public class ProductImageResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;
        public ProductImageResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _configuration["ApiUrl"] + source.PictureUrl;
            }
            else
            {
                return _configuration["ApiUrl"] + "Resources/Images/Default.png";
            }
        }
    }
}
