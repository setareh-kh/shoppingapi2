using Microsoft.AspNetCore.Mvc;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Models;
using shoppingapi2.Services;

namespace shoppingapi2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();

        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpGet("mobile/{mobile}")]
    public async Task<IActionResult> GetByMobile(string mobile)
    {
        var user = await _userService.GetByMobileAsync(mobile);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateUserDto dto)
    {
        var result = await _userService.CreateAsync(dto);

        if (result == null)
            return Conflict("A user with this mobile already exists.");

        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id,UpdateUserDto dto)
    {
        var result = await _userService.UpdateAsync(id, dto);

        if (!result)
            return NotFound();

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _userService.DeleteAsync(id);

        if (!result)
            return NotFound();

        return NoContent();
    }
}
