using Microsoft.AspNetCore.Mvc;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Services;
using shoppingapi2.Setting;
using shoppingapi2.Validators;

namespace shoppingapi2.Controllers.Admin;
[Authorize(Type = new int[] { 0 })]
[ApiController]
[Route(ApiRoutes.Admin.Category)]
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
        if (result!.Count==0 ) return Ok(result);
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

    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateCategoryDto dto)
    {
        var category = await _categoryService.CreateAsync(dto);
        if (category == null)
            return BadRequest();
        await _redisService.SetValueAsync($"Category_{category.Id}", category, TimeSpan.FromMinutes(10));
        return Ok(category);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateCategoryDto dto)
    {
        var result = await _categoryService.UpdateAsync(id, dto);
        if (!result)
            return NotFound();
        await _redisService.SetValueAsync($"Category_{id}", result, TimeSpan.FromMinutes(10));
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _categoryService.DeleteAsync(id);
        if (!result)
            return NotFound();
        await _redisService.DeleteKeyAsync($"Category_{id}");
        return NoContent();
    }
}