using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Models;
using shoppingapi2.Repositories;

namespace shoppingapi2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository productRepository, IMapper mapper, IImageRepository imageRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _imageRepository = imageRepository;
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddAsync([FromForm] AddProductDto addProductDto)
        {
            var product = await _productRepository.AddAsync(addProductDto);
            var response = _mapper.Map<AdminProductResponseDto>(product);
            List<Image> imgLst = new();
            foreach (var item in addProductDto.Files)
            {
                imgLst.Add(await _imageRepository.SaveAsync(item, "Product", product.Id));
            }
            response.Images = imgLst;
            return Ok(response);
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            if (products != null)
            {
                var responses = products?.Select(x => _mapper.Map<AdminProductResponseDto>(x)).ToList();
                foreach (var r in responses!)
                {
                    r.Images = await _imageRepository.GetImagesAsync("Product", r.Id);
                }
                return Ok(responses);
            }
            else
                return Ok("No Any exisit user");
        }
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product != null)
            {
                var response = _mapper.Map<AdminProductResponseDto>(product);
                response.Images = await _imageRepository.GetImagesAsync("Product", product.Id);
                return Ok(response);
            }
            return Ok($"product by {id} is not found!!");

        }
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] UpdateProductDto updateProductDto)
        {
            var productResult = await _productRepository.UpdateAsync(id, updateProductDto);
            if (!productResult)
                return Ok($"{id} number is not found!!");
            //update images for this product
            List<bool> imageResult = new();
            foreach (var file in updateProductDto.Files)
                imageResult.Add(await _imageRepository.UpdateAsync(file, "Product", id));
            if (imageResult.All(x => x == true))
                return Ok($"successfully updated the product and it's image");
            else
                return Ok($"Failed to update Images!");
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var productResult = await _productRepository.DeleteAsync(id);
            if (!productResult)
                return Ok($"{id}number is not found!!");
            var imageResult = await _imageRepository.DeleteAsync("Product", id);
            if (imageResult)
                return Ok("Successfuly deleted the product and it's image");
            else
                return Ok("Failed to delete Image");

        }
    }
}