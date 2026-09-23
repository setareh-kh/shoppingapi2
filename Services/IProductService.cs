using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;

namespace shoppingapi2.Services;

public interface IProductService
{
    Task<AdminProductResponseDto?> GetByIdAsync(int id);
    Task<List<AdminProductResponseDto>> GetAllAsync();
    Task<AdminProductResponseDto?> CreateAsync(CreateProductDto dto);
    Task<bool> UpdateAsync(int id, UpdateProductDto dto);
    Task<bool> DeleteAsync(int id);

}