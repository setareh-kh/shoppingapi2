using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Models;
using shoppingapi2.Repositories;

namespace shoppingapi2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddAsync([FromForm] AddProductDto addProductDto)
        {
            var product = await _productRepository.AddAsync(addProductDto);
            return Ok(_mapper.Map<UserProductResponseDto>(product));
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);//?.Count > 0 ? products.Select(x=>_mapper.Map<UserProductResponseDto>(x)) : "No Any exisit user");
        }
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return Ok(product != null ? _mapper.Map<UserProductResponseDto>(product) : $"{id}number is not found!!");

        }
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateProductDto updateProductDto)
        {
            var result = await _productRepository.UpdateAsync(id, updateProductDto);
            return Ok(result == true ? $"{id} number is updated" : $"{id}number is not found!!");
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _productRepository.DeleteAsync(id);
            return Ok(result == true ? $"{id} number is deleted" : $"{id}number is not found!!");
        }
    }
}