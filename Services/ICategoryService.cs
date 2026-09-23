using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;

namespace shoppingapi2.Services;

public interface ICategoryService
{
    Task<CategoryResponseDto?> GetByIdAsync(int id);
    Task<List<CategoryResponseDto>> GetAllAsync();
    Task<CategoryResponseDto?> CreateAsync(CreateCategoryDto dto);
    Task<bool> UpdateAsync(int id,UpdateCategoryDto dto);
    Task<bool> DeleteAsync(int id);
}