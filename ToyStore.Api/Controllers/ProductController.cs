using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using ToyStore.Api.DTOS;
using ToyStore.Api.Errors;
using ToyStore.Api.Helpers;
using ToyStore.Core.IRepository;
using ToyStore.Core.Models;
using ToyStore.Core.SharedModels;


namespace ToyStore.Api.Controllers
{
    [Authorize(Roles ="Admin")]
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

        [AllowAnonymous]
        [HttpGet("getProduct/{id}")]
        public async Task<ActionResult> getProduct(int id)
        {
            var pr = await _unitOfWork._productRepo.GetProductByIdAsync(id);
            var prDto = _mapper.Map<ProductDto>(pr);
            return Ok(prDto);
        }

        [AllowAnonymous]
        [HttpPost("getAllProducts")]
        public async Task<ActionResult> getProductsByCategory(PaginationDto pagination)
        {
            var pr = await _unitOfWork._productRepo.GetProductsAsync(pagination);
            int totalCount = await _unitOfWork._productRepo.productCount(pagination);
            var prDto = _mapper.Map<List<ProductDto>>(pr);
            return Ok(new ProductPaginationDto() { PageCount= (int)Math.Ceiling(totalCount*1.0/ pagination.PageSize), PageNumber= pagination.PageIdx,
                PageSize=prDto.Count(),
            Products = prDto});
        }

        [HttpPost("getProducts")]
        [AllowAnonymous]
        public async Task<IActionResult> getProducts(GenericPagination pagination)
        {
            var products = await _unitOfWork._paginationRepo.
                GetDataByDynamicPropertyAsync<Product>(pagination,new List<string>{ "colors","Sizes","Category"});
            var prDto = _mapper.Map<List<ProductDto>>(products);
            return Ok(prDto);
        }

        [HttpPost("addProduct")]
        public async Task<ActionResult> addProduct(AddProductDto model)
        {
            var product = _mapper.Map<Product>(model);
            await addColors(product , model.ColorId);
            await addSizes(product , model.SizeId);   
            await _unitOfWork._productRepo.AddAsync(product);
            var fileName = await uploadPhoto(model.Image);
            product.PictureUrl = fileName;
            if (!await _unitOfWork.saveChangesAsync())
            {
                return BadRequest(400);
            }
            return Created();
        }

        private async Task addColors(Product product,List<int>colors)
        {
            var col = await _unitOfWork._colorRepo.GetColorsAsync(colors);
            product.colors = col.ToList();
        }
        private async Task addSizes(Product product, List<int> sizes)
        {
            var siz = await _unitOfWork._sizeRepo.GetSizesAsync(sizes);
            product.Sizes = siz.ToList();
        }

        [HttpPut("updateProduct/{productId}")]
        public async Task<ActionResult> updateProduct(int productId,updateProductDto model)
        {
            var product = await _unitOfWork._productRepo.GetProductByIdAsync(productId);
            if (product == null) {
                return NotFound(new ApiResponse(404));
            }
            if (product.Id != model.Id) {
                return BadRequest(new ApiResponse(400));
            }
            product.SellPrice = model.SellPrice;
            product.MainPrice = model.MainPrice;
            product.Description = model.Description;
            product.CategotyId = model.CategotyId;
            product.Name = model.Name;

            FileHelper file = new FileHelper(_env);
            if (model.Image != null && model.Image.Length > 0)
            {
                file.DeleteImage(product.PictureUrl);
                var res = file.SaveImage(model.Image);
                if(res.Item1 == 0)
                {
                    return BadRequest(new ApiResponse(400, res.Item2));
                }
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
