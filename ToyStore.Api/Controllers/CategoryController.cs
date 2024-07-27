using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToyStore.Api.DTOS;
using ToyStore.Api.Errors;
using ToyStore.Core.IRepository;
using ToyStore.Core.Models;

namespace ToyStore.Api.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public CategoryController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _mapper = mapper;
            _uow = unitOfWork;  
        }

        [HttpGet("getAll")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _uow._categoryRepo.GetCategoriesAsync());
        }

        [HttpGet("getCategory/{id}")]
        public async Task<ActionResult> getCategory(int id)
        {
            return Ok(await _uow._categoryRepo.GetCategoryByIdAsync(id));
        }

        [HttpPost("addCategory")]
        public async Task<ActionResult> addCategory(AddCategoryDto model)
        {
            var category = _mapper.Map<Category>(model);
            await _uow._categoryRepo.AddAsync(category);
            if (!await _uow.saveChangesAsync()) {
                return BadRequest(new ApiResponse(400));
            }
            return Created();
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
            if(!await _uow.saveChangesAsync())
            {
                return BadRequest(new ApiResponse(400));
            }
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
            _uow._categoryRepo.Delete(category);
            if (!await _uow.saveChangesAsync())
            {
                return BadRequest(new ApiResponse(400));
            }
            return Created();
        }
    }

}
