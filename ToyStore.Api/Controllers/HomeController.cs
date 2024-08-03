using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToyStore.Api.DTOS;
using ToyStore.Api.Helpers;
using ToyStore.Core.IRepository;

namespace ToyStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
       
        public HomeController(IUnitOfWork uow , IMapper mapper,IConfiguration configuration , IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;  
            _uow = uow;
            _config = configuration;
            _host = webHostEnvironment;
        }

        [HttpGet("getHomeData")]
        public async Task<IActionResult> getHomeData()
        {
            var cat = await _uow._categoryRepo.GetCategoriesAsync();
            var catDto = _mapper.Map<List<CategoryDto>>(cat);
            var homeData = new HomeDataDto()
            {
                Categories = catDto,
                MainImage = _config["ApiUrl"] + "Resources/Images/MainImage.jpg"
            };
            return Ok(homeData);
        }

    }
}
