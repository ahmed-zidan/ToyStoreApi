using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToyStore.Api.DTOS;
using ToyStore.Api.Errors;
using ToyStore.Api.Extensions;
using ToyStore.Core.IRepository;
using ToyStore.Core.Models;

namespace ToyStore.Api.Controllers
{
   
    public class MenuController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public MenuController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _mapper = mapper;
            _uow = unitOfWork;
        }

        [HttpGet("getMenusByRole")]
        [Authorize]
        public async Task<IActionResult> getMenusByRole() {
            string roleName = HttpContext.getCurrentUserRole();
            var menus = await _uow._menuRepo.getAllMenus(roleName);
            var MenusDto = _mapper.Map<List<MenuDto>>(menus);
            return Ok(MenusDto);
        }

        [HttpGet("getMenuAccess/{menuId}")]
        [Authorize]
        public async Task<IActionResult> getMenuAccess(int menuId)
        {
            string roleName = HttpContext.getCurrentUserRole();
            var menus = await _uow._menuRepo.getMenuAccess(roleName,menuId);
            var MenuAccessDto = _mapper.Map<MenuAccessDto>(menus);
            return Ok(MenuAccessDto);
        }

        /*[HttpGet("getMenus")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> getMenus()
        {
            var Menus = await _uow._menuRepo.getAllMenus();
            var MenusDto = _mapper.Map<List<MenuDto>>(Menus);
            return Ok(MenusDto);
        }*/

        [HttpPost("addMenu")]
        [Authorize]
        public async Task<IActionResult> addMenu(string name)
        {
            if(await _uow._menuRepo.isMenuExistAsync(name))
            {
                return BadRequest(new ApiResponse(400,"menu already exist"));
            }
            
            await _uow._menuRepo.addMenu(new Core.Models.Menu { Name = name });
            await _uow.saveChangesAsync();
            return Ok();
        }

        [HttpPost("updateMenu")]
        [Authorize]
        public async Task<IActionResult> updateMenu(MenuDto menu)
        {
            var res = await _uow._menuRepo.getMenu(menu.Id);
            if(res == null)
            {
                return BadRequest(new ApiResponse(400, "menu not exist"));
            }
            if(res.Name == menu.Name)
            {
                return Ok();
            }
            if(await _uow._menuRepo.isMenuExistAsync(menu.Name))
            {
                return BadRequest(new ApiResponse(400, "menu already exist"));
            }    
           
            res.Name = menu.Name;
            await _uow.saveChangesAsync();
            return Ok();
        }

        [HttpDelete("deleteMenu/{id}")]
        [Authorize]
        public async Task<IActionResult> deleteMenu(int id)
        {
            var res = await _uow._menuRepo.getMenu(id);
            if (res == null)
            {
                return BadRequest(new ApiResponse(400, "menu not exist"));
            }
            _uow._menuRepo.deleteMenu(res);
            await _uow.saveChangesAsync();
            return Ok();
        }


    }
}
