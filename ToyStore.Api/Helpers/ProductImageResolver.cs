using AutoMapper;
using AutoMapper.Execution;
using ToyStore.Api.DTOS;
using ToyStore.Core.Models;

namespace ToyStore.Api.Helpers
{
    public class ProductImageResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductImageResolver(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.PictureUrl)) {
                return "";
            }
            else
            {
                return new FileHelper(_hostEnvironment).getFullPath(source.PictureUrl);
            }
        }
    }
}
