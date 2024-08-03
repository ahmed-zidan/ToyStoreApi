using AutoMapper;
using AutoMapper.Execution;
using ToyStore.Api.DTOS;
using ToyStore.Core.Models;

namespace ToyStore.Api.Helpers
{
    public class CategoryImageResolver : IValueResolver<Category, CategoryDto, String>
    {
        private readonly IConfiguration _config;
        public CategoryImageResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(Category source, CategoryDto destination, string destMember, ResolutionContext context)
        {
            if (source.Image != null && source.Image != "") { 
                return _config["ApiUrl"] + source.Image;
            }
            else
            {
                return _config["ApiUrl"] + "Resources/Images/Default.png";
            }
        }
    }
}
