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
    public class SizeController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        public SizeController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _uow = unitOfWork;
        }

        [HttpGet("get")]
        public async Task<IActionResult> get()
        {
            var sizes = await _uow._sizeRepo.GetSizesAsync();
            return Ok(_mapper.Map<List<ListOFIdAndName>>(sizes));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(string name)
        {
            if (await _uow._sizeRepo.IsExist(name))
            {
                return BadRequest(new ApiResponse(400, "size is already exists"));
            }
            await _uow._sizeRepo.AddSizeAsync(new Size() { Name = name });
            if (!await _uow.saveChangesAsync())
            {
                return BadRequest(400);
            }
            return Ok();
        }

        [HttpDelete("Remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var size = await _uow._sizeRepo.Get(id);
            if (size == null)
            {
                return NotFound();
            }
            await _uow._sizeRepo.AddSizeAsync(size);
            if (!await _uow.saveChangesAsync())
            {
                return BadRequest(400);
            }
            return Ok();
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(int id, string name)
        {
            var size = await _uow._sizeRepo.Get(id);
            if (size == null)
            {
                return NotFound();
            }
            size.Name = name;
            if (!await _uow.saveChangesAsync())
            {
                return BadRequest(400);
            }
            return Ok();
        }
    }
}
