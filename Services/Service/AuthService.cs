using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Repositories;

namespace shoppingapi2.Services.Service;
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponseDto?> LoginAsync(LoginRequestDto dto)
    {
        var user = await _userRepository.FindAsync(x => x.Mobile == dto.Mobile);
        if (user == null || user.Password != dto.Password)
            return null;

        return new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Mobile = user.Mobile,
            Type = user.Type
        };
    }


}



