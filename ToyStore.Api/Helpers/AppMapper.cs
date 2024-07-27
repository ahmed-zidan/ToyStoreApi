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
        }
    }
}
