using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToyStore.Api.DTOS;
using ToyStore.Api.Errors;
using ToyStore.Api.Extensions;
using ToyStore.Core.IRepository;
using ToyStore.Core.Models;
using ToyStore.Core.Models.Orders;

namespace ToyStore.Api.Controllers
{
  
    public class OrderController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IBasketService _basket;
        public OrderController(IMapper mapper , IUnitOfWork unitOfWork, IBasketService basket)
        {
            _mapper = mapper;
            _uow = unitOfWork;
            _basket = basket;
        }

        [HttpPost("addOrder")]
        [Authorize]
        public async Task<IActionResult> addOrder(AddOrderDto model)
        {
            var basket = await _basket.getBasket(model.basketId);
            if (basket == null)
            {
                return NotFound(new ApiResponse(404 , "Basket not found"));
            }
            var deliveryMethod = await _uow._deliveryRepo.getDeliveryMethodAsync(model.deliveryMethode);
            if (deliveryMethod == null)
                return NotFound(new ApiResponse(404,"Delivery method not found"));

            var shipAddress = _mapper.Map<ShipAddress>(model.shippingAddress);
            var buyerEmail = HttpContext.getCurrentUserEmail();
            var order = await _uow._orderRepo.AddOrderAsync(basket, deliveryMethod, shipAddress,buyerEmail);
            await _basket.deleteBasket(model.basketId);
            await _uow.saveChangesAsync(); 
            return Ok(order);
        }

        [HttpGet("getOrders")]
        [Authorize]
        public async Task<IActionResult> getOrders()
        {
            var userEmail = HttpContext.getCurrentUserEmail();
            var order = await _uow._orderRepo.getOrdersAync(userEmail);
            if(order == null)
            {
                return NotFound(new ApiResponse(404, "No Orders"));
            }
            
            return Ok(order);   
        }
    }
}
