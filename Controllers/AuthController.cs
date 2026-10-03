using Microsoft.AspNetCore.Mvc;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Services;
using shoppingapi2.Setting;


[ApiController]
[Route(ApiRoutes.Website.Auth)]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto loginDto)
    {
        var result = await _authService.LoginAsync(loginDto);
        if (result == null)
            return Unauthorized();
        return Ok(result);
    }

    
   
}