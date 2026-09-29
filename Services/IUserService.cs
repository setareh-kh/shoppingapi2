using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Models;

namespace shoppingapi2.Services;

public interface IUserService
{
    Task<PaginateResponseDto<UserResponseDto>> Filter(UserFilterDto filter);
    Task<UserUserResponseDto?> GetByIdAsync(int id);
    Task<List<UserUserResponseDto>> GetAllAsync();
    Task<AdminUserResponseDto?> GetByMobileAsync(string mobile);
    Task<UserUserResponseDto?> CreateAsync(CreateUserDto dto);
    Task<bool> UpdateAsync(int id, UpdateUserDto dto);
    Task<bool> DeleteAsync(int id);
}