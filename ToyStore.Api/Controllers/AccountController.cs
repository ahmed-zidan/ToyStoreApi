using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Security.Claims;
using ToyStore.Api.DTOS;
using ToyStore.Api.Errors;
using ToyStore.Api.Extensions;
using ToyStore.Core.Models;
using ToyStore.Core.Repository;

namespace ToyStore.Api.Controllers
{

    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper,
            IJWTService jwtService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _jwtService = jwtService;
            _signInManager = signInManager;
        }

        [HttpGet("getAllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> getAllUsers()
        {
            return Ok(await _userManager.Users.ToListAsync());
        }

        [HttpGet("getUser/{id}")]
        [Authorize]
        public async Task<IActionResult> getUser(string id)
        {
            var user = await _userManager.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (user == null) {
                return NotFound(new ApiResponse(404));
            }
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) {
                return Unauthorized(new ApiResponse(401, "Email or password is not correct"));
            }
            var res = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (res.Succeeded) {
                return Ok(new UserTokenDto()
                {
                    DisplayName = user.DisplayName,
                    UserId = user.Id,
                    Token = _jwtService.GetToken(user)
                });
            }
            else
            {
                return Unauthorized(new ApiResponse(401, "Email or password is not correct"));
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> register(RegisterDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                return BadRequest(new ApiResponse(400, "Email is already taken"));
            }

            var userModel = _mapper.Map<AppUser>(model);
            var res = await _userManager.CreateAsync(userModel, model.Password);
            if (res.Succeeded) {
                await _userManager.AddToRoleAsync(userModel, "User");
                return Created();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("addAddress")]
        [Authorize]
        public async Task<IActionResult> addAddress(AddAddressDto model)
        {
            var currUserId = HttpContext.getCurrentUserIdentifier();
            var user = await _userManager.Users.Include(x=>x.Address).FirstOrDefaultAsync(x=>x.Id == currUserId);
            if (user == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            var address = _mapper.Map<Address>(model);
            if(user.Address == null)
            {
                user.Address = address;
            }
            else
            {
                address.Id = user.Address.Id;
                user.Address = address;
            }
            var res = await _userManager.UpdateAsync(user);
            if (res.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("getAddress")]
        [Authorize]
        public async Task<IActionResult> getAddress()
        {
            var userId = HttpContext.getCurrentUserIdentifier();
            var user = await _userManager.Users.Include(x=>x.Address).FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) {
                return BadRequest(new ApiResponse(400));
            }
            if (user.Address == null) {
                return Ok(new Address());
            }
            var addressDto = _mapper.Map<AddressDto>(user.Address); 
            return Ok(addressDto);
        }


    }
}
