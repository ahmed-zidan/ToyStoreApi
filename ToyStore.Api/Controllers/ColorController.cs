using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToyStore.Api.DTOS;
using ToyStore.Api.Errors;
using ToyStore.Core.IRepository;
using ToyStore.Core.Models;

namespace ToyStore.Api.Controllers
{
    //[Authorize]
    public class ColorController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        public ColorController(IMapper mapper , IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _uow = unitOfWork;
        }

        [HttpGet("get")]
        public async Task<IActionResult> get()
        {
            var colors = await _uow._colorRepo.GetColorsAsync();
            return Ok(_mapper.Map <List<ColorDto>>(colors));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(string name)
        {
            if (await _uow._colorRepo.IsExist(name))
            {
                return BadRequest(new ApiResponse(400, "color is already exists"));
            }
            await _uow._colorRepo.AddColorAsync(new Color() { Name = name});
            if (!await _uow.saveChangesAsync()) {
                return BadRequest(400);
            }
            return Ok();
        }

        [HttpDelete("Remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var color = await _uow._colorRepo.Get(id);
            if (color == null) {
                return NotFound();
            }
            await _uow._colorRepo.AddColorAsync(color);
            if (!await _uow.saveChangesAsync())
            {
                return BadRequest(400);
            }
            return Ok();
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(int id , string name)
        {
            var color = await _uow._colorRepo.Get(id);
            if (color == null)
            {
                return NotFound();
            }
            color.Name = name;
            if (!await _uow.saveChangesAsync())
            {
                return BadRequest(400);
            }
            return Ok();
        }
    }
}
