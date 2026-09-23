using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Models;

namespace shoppingapi2.Services;

public interface IUserService
{
    Task<UserUserResponseDto?> GetByIdAsync(int id);

    Task<List<UserUserResponseDto>> GetAllAsync();

    Task<UserUserResponseDto?> GetByMobileAsync(string mobile);

    Task<UserUserResponseDto?> CreateAsync(CreateUserDto dto);

    Task<bool> UpdateAsync(int id, UpdateUserDto dto);

    Task<bool> DeleteAsync(int id);
}