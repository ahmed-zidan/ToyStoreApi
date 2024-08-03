using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using ToyStore.Api.DTOS;
using ToyStore.Api.Errors;
using ToyStore.Api.Helpers;
using ToyStore.Core.IRepository;
using ToyStore.Core.Models;
using ToyStore.Infrastructure.Repo;

namespace ToyStore.Api.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public ProductController(IUnitOfWork unitOfWork , IMapper mapper, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }

        [HttpGet("getProduct/{id}")]
        public async Task<ActionResult> getProduct(int id)
        {
            var pr = await _unitOfWork._productRepo.GetProductByIdAsync(id);
            var prDto = _mapper.Map<ProductDto>(pr);
            return Ok(prDto);
        }

        [HttpGet("getAllProducts")]
        public async Task<ActionResult> getProductsByCategory(int ?categoryId , string?search , string ?sorting, int pageIdx = 0,int pageSize = 10)
        {
            var pr = await _unitOfWork._productRepo.GetProductsAsync(categoryId,search, sorting , pageIdx,pageSize);
            int totalCount = await _unitOfWork._productRepo.productCout(categoryId,search);
            var prDto = _mapper.Map<List<ProductDto>>(pr);
            return Ok(new ProductPaginationDto() { PageCount= (int)Math.Ceiling(totalCount*1.0/pageSize), PageNumber=pageIdx,PageSize=prDto.Count(),
            Products = prDto});
        }

        [HttpPost("addProduct")]
        public async Task<ActionResult> addProduct(AddProductDto model)
        {
            var product = _mapper.Map<Product>(model);
            var fileName = await uploadPhoto(model.Image);
            product.PictureUrl = fileName;
            await _unitOfWork._productRepo.AddAsync(product);
            if(!await _unitOfWork.saveChangesAsync())
            {
                return BadRequest(400);
            }
            return Created();
        }

        [HttpPut("updateProduct")]
        public async Task<ActionResult> updateProduct(int productId,updateProductDto model)
        {
            var product = await _unitOfWork._productRepo.GetProductByIdAsync(productId);
            if (product == null) {
                return NotFound(new ApiResponse(404));
            }
            if (product.Id != model.Id) {
                return BadRequest(new ApiResponse(400));
            }
            product.Price = model.Price;
            product.Description = model.Description;
            product.CategotyId = model.CategotyId;
            product.Price = product.Price;
            product.Name = model.Name;

            FileHelper file = new FileHelper(_env);
            if (model.Image != null && model.Image.Length > 0)
            {
                file.DeleteImage(product.PictureUrl);
                var res = file.SaveImage(model.Image);
                product.PictureUrl = res.Item2;
            }

            if (!await _unitOfWork.saveChangesAsync())
            {
                return BadRequest(400);
            }
            return Ok();
        }

        [HttpDelete("deleteProduct/{id}")]
        public async Task<ActionResult> deleteProduct(int id)
        {
            var pr = await _unitOfWork._productRepo.GetProductByIdAsync(id);
            if (pr == null) { 
                return NotFound(new ApiResponse(404));
            }
            FileHelper file = new FileHelper(_env);
            file.DeleteImage(pr.PictureUrl);
            _unitOfWork._productRepo.Delete(pr);
            if (!await _unitOfWork.saveChangesAsync()) {
                return BadRequest(new ApiResponse(400));
            }
            return Ok();
        }

        private async Task<string> uploadPhoto(IFormFile file)
        {
            if (file.Length > 0)
            {
                var fileHelper = new FileHelper(_env);
                var res = fileHelper.SaveImage(file);
                return res.Item2;
            }
            else
            {
                return "";
            }

        }
    }
}
