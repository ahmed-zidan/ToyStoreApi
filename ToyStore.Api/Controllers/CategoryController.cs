using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToyStore.Api.DTOS;
using ToyStore.Api.Errors;
using ToyStore.Api.Helpers;
using ToyStore.Core.IRepository;
using ToyStore.Core.Models;
using ToyStore.Infrastructure.Repo;

namespace ToyStore.Api.Controllers
{
    [Authorize(Roles ="Admin")]
    
    public class CategoryController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CategoryController(IUnitOfWork unitOfWork , IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _mapper = mapper;
            _uow = unitOfWork;  
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet("getAll")]
        [AllowAnonymous]
        public async Task<ActionResult> GetAll()
        {
            var cat = await _uow._categoryRepo.GetCategoriesAsync();
            return Ok(_mapper.Map<List<CategoryDto>>(cat));
        }

        [HttpGet("getCategory/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> getCategory(int id)
        {
            var cat = await _uow._categoryRepo.GetCategoryByIdAsync(id);
            return Ok(_mapper.Map<CategoryDto>(cat));
        }

        [HttpPost("addCategory")]
        public async Task<ActionResult> addCategory([FromForm] AddCategoryDto model)
        {
            var category = _mapper.Map<Category>(model);
            FileHelper fileHelper = new FileHelper(_hostEnvironment);
            var path = fileHelper.SaveImage(model.Image);
            if (path.Item1 == 0)
            {
                return BadRequest(new ApiResponse(400, "Image Not Saved"));
            }
            category.Image = path.Item2;
            await _uow._categoryRepo.AddAsync(category);
            if (!await _uow.saveChangesAsync()) {
                return BadRequest(new ApiResponse(400));
            }
            return Ok();
        }

        [HttpPut("updateCategory/{id}")]
        public async Task<ActionResult> updateCategory(int id,UpdateCategoryDto model)
        {
            var category = await _uow._categoryRepo.GetCategoryByIdAsync(id);
            if(category == null)
            {
                return NotFound(new ApiResponse(404));
            }
            if(category.Id != model.Id)
            {
                return BadRequest(new ApiResponse(400));
            }
            category.Description = model.Description;
            category.Name = model.Name;
            
            
            if (model.Image != null && model.Image.Length > 0)
            {
                FileHelper fileHelper = new FileHelper(_hostEnvironment);
                fileHelper.DeleteImage(category.Image);
                var res = fileHelper.SaveImage(model.Image);
                category.Image= res.Item2;
            }
            await _uow.saveChangesAsync();
            /*{
                
                return BadRequest(new ApiResponse(400));
            }*/
            return Ok();
        }

        [HttpDelete("deleteCategory/{id}")]
        public async Task<ActionResult> deleteCategory(int id)
        {
            var category = await _uow._categoryRepo.GetCategoryByIdAsync(id);
            if(category == null)
            {
                return NotFound(new ApiResponse(404));
            }
            FileHelper file = new FileHelper(_hostEnvironment);
            file.DeleteImage(category.Image);
            _uow._categoryRepo.Delete(category);
            if (!await _uow.saveChangesAsync())
            {
                return BadRequest(new ApiResponse(400));
            }
            return NoContent();
        }
    }

}
