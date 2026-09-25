using Microsoft.AspNetCore.Mvc;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Services;

namespace shoppingapi2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    [HttpGet("Filter")]
    public async Task<IActionResult> Filter(ProductFilterDto filterDto)
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

    [HttpPost("Add")]
    public async Task<IActionResult> Create([FromForm] CreateProductDto dto)
    {
        var product = await _productService.CreateAsync(dto);

        if (product == null)
            return BadRequest("Category does not exist.");

        return Ok(product);
    }

    [HttpPut("Update/{id:int}")]
    public async Task<IActionResult> Update([FromForm] UpdateProductDto dto, int id)
    {
        var result = await _productService.UpdateAsync(id, dto);

        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("Delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _productService.DeleteAsync(id);

        if (!result)
            return NotFound();

        return NoContent();
    }
}