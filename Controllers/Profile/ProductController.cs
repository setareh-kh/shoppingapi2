using Microsoft.AspNetCore.Mvc;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Services;
using shoppingapi2.Setting;

namespace shoppingapi2.Controllers.Profile;

[ApiController]
[Route(ApiRoutes.Profile.Product)]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("Filter")]
    public async Task<IActionResult> Filter([FromQuery] ProductFilterDto filterDto)
    {
        var result = await _productService.Filter(filterDto);
            return Ok(result);
    }


    [HttpGet("All")]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllAsync();

        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
            return NotFound();

        return Ok(product);
    }

}