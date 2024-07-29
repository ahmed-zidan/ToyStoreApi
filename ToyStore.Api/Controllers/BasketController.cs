using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToyStore.Api.DTOS;
using ToyStore.Api.Errors;
using ToyStore.Core.IRepository;
using ToyStore.Core.Models;

namespace ToyStore.Api.Controllers
{
   
    public class BasketController : BaseController
    {
        private readonly IBasketService _basket;
        private readonly IMapper _mapper;
        public BasketController(IBasketService basket,IMapper mapper)
        {
            _basket = basket;
            _mapper = mapper;
        }

        [HttpGet("getBasket/{id}")]
        public async Task<IActionResult> getBasket(string id)
        {
            var basket = await _basket.getBasket(id);
            return Ok(basket ?? new Basket(id));

        }

        [HttpPut("updateBasket")]
        public async Task<IActionResult> updateBasket(CustomerBasketDto model)
        {
            var bask = _mapper.Map<Basket>(model);
            var basket = await _basket.updateBasket(bask);
            if (basket != null)
            {
                return Ok(basket);
            }
            else
            {
                return BadRequest(new ApiResponse(400, "Bad Request"));
            }
        }
        
        [HttpDelete("deleteBasket/{id}")]
        public async Task<IActionResult> deleteBasket(string id)
        {
            var deleted = await _basket.deleteBasket(id);
            if (deleted)
            {
                return Ok();
            }
            else
            {
                return NotFound(new ApiResponse(400, "Bad Request"));
            }
        }
    }
}
