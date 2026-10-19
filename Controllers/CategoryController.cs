using Microsoft.AspNetCore.Mvc;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Services;
using shoppingapi2.Setting;


[ApiController]
[Route(ApiRoutes.Website.Category)]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IRedisService _redisService;
    public CategoryController(ICategoryService catogoryService, IRedisService redisService)
    {
        _categoryService = catogoryService;
        _redisService = redisService;
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _redisService.GetValueAsync<List<CategoryResponseDto>>($"All_categories");
        if (result != null) return Ok(result);
        result = await _categoryService.GetAllAsync();
        await _redisService.SetValueAsync($"All_categories", result, TimeSpan.FromMinutes(10));
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _redisService.GetValueAsync<CategoryResponseDto>($"Category_{id}");
        if (result != null) return Ok(result);
        result = await _categoryService.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        await _redisService.SetValueAsync($"Category_{id}", result, TimeSpan.FromMinutes(10));
        return Ok(result);
    }

}