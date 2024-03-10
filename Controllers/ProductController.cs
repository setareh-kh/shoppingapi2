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
            List<Image> imgLst=new();
            foreach(var item in addProductDto.Files )
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
                    r.Images=await _imageRepository.GetImagesAsync("Product",r.Id);
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
            if(product != null)
            {
                var response=_mapper.Map<AdminProductResponseDto>(product);
                response.Images=await _imageRepository.GetImagesAsync("Product",product.Id);
                return Ok(response);
            }
            return Ok($"product by {id} is not found!!");

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
            var resultproduct = await _productRepository.DeleteAsync(id);
            var rsltImg=await _imageRepository.DeleteAsync("Product",id);
            if (rsltImg==true) {
                if(resultproduct==true)
                    return Ok("Successfully both of deleted");
                else 
                return Ok("Falied product, succssfully images");

            } 
            else
            {
                if(resultproduct==true)
                    return Ok("Falied images , succssfully product");
                else 
                return Ok("Falied both of them");

            }
        }
    }
}