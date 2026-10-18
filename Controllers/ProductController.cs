using Microsoft.AspNetCore.Mvc;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Models;
using shoppingapi2.Services;
using shoppingapi2.Setting;

namespace shoppingapi2.Controllers;

[ApiController]
[Route(ApiRoutes.Website.Product)]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IRedisService _redisService;

    public ProductController(IProductService productService, IRedisService redisService)
    {
        _productService = productService;
        _redisService = redisService;
    }

    [HttpGet("Filter")]
    public async Task<IActionResult> Filter([FromQuery] ProductFilterDto filterDto)
    {
        var result = await _redisService.
            GetValueAsync<PaginateResponseDto<ProductResponseDto>>
            ($"Product_result_Filter");
        if (result != null) return Ok(result);
        result = await _productService.Filter(filterDto);
        await _redisService.SetValueAsync($"Product_result_Filter", result, TimeSpan.FromMinutes(10));
        return Ok(result);
    }


    [HttpGet("All")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _redisService.
            GetValueAsync<List<AdminProductResponseDto>>($"All_Products_4Admin");
        if (result != null) return Ok(result);
        result = await _productService.GetAllAsync();
        await _redisService.SetValueAsync($"All_Products_4Admin", result, TimeSpan.FromMinutes(10));
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _redisService.GetValueAsync<AdminProductResponseDto>($"Product_{id}_4Admin");
        if (result != null) return Ok(result);
        result = await _productService.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        await _redisService.SetValueAsync($"Product_{id}_4Admin", result, TimeSpan.FromMinutes(10));
        return Ok(result);
    }
}