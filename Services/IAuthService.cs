using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;

namespace shoppingapi2.Services;

public interface IAuthService
{
   Task<UserResponseDto?> LoginAsync(LoginRequestDto dto);
}