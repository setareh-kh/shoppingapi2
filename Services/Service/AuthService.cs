using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DeviceDetectorNET;
using DeviceDetectorNET.Cache;
using Microsoft.IdentityModel.Tokens;
using shoppingapi2.Dtos.Objects;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Extentions;
using shoppingapi2.Models;
using shoppingapi2.Repositories;

namespace shoppingapi2.Services.Service;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly AppSettings _appSettings;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITokenRepository _tokenRepository;

    public AuthService(IUserRepository userRepository,
         AppSettings appSettings, IHttpContextAccessor httpContextAccessor,
         ITokenRepository tokenRepository)
    {
        _userRepository = userRepository;
        _appSettings = appSettings;
        _httpContextAccessor = httpContextAccessor;
        _tokenRepository= tokenRepository;
    }
    private const int LimitTokenDays = 15;

    public async Task<StandardResponseDto> LoginAsync(LoginRequestDto dto)
    {
        var user = await _userRepository.FindAsync(x => x.Mobile == dto.Mobile);
        if (user == null) 
            return new StandardResponseDto { Success = false, Message = "User not found" };
        if (user!.Password != dto.Password)
            return new StandardResponseDto { Success = false, Message = "Password not correct" };
            
        var token = await GenerateJwtToken(user);
        return new StandardResponseDto
            {
                Success = true, Message = "", Object = new
                {
                    Id = user.Id,
                    Name = user.Name,
                    Token = token
                }
            };

    }

    private async Task<string> GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret!);
        var expire = DateTime.Now.AddDays(LimitTokenDays);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("mobile", user.Mobile ?? ""),
                new Claim("type", user.Type.ToString() ?? "")
            }),
            Expires = expire,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        string userAgent = (_httpContextAccessor.HttpContext?.Request.Headers.UserAgent ?? "")!;
        var dd = new DeviceDetector(userAgent);
        dd.SetCache(new DictionaryCache());
        dd.Parse();
        var os = dd.GetOs()?.Match;
        var clientMatch = dd.GetBrowserClient()?.Match;
        var t = new Token
        {
            UserId = user.Id,
            Hash = jwt,
            CreatedAt = DateTime.Now,
            IpAddress = _httpContextAccessor.HttpContext?.GetRealClientIpAddress(),
            ExpiredAt = expire,
            Os = $"{os?.Name ?? "os"} {os?.Version ?? ""}",
            Browser = clientMatch?.Name ?? "browser",
            Active = true
        };

        await _tokenRepository.InsertAsync(t);
        await _tokenRepository.SaveChangesAsync();

        return jwt;
    }


}



