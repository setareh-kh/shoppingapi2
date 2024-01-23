using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Repositories;

namespace shoppingapi2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddAsync([FromBody] AddUserDto addUserDto)
        {
            var user = await _userRepository.AddAsync(addUserDto);
            return Ok(_mapper.Map<UserUserResponseDto>(user));
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users?.Count > 0 ? users.Select(x => _mapper.Map<UserUserResponseDto>(x)) : "No Any exisit user");
        }
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return Ok(user != null ? _mapper.Map<UserUserResponseDto>(user) : $"{id}number is not found!!");

        }
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateUserDto updateUserDto)
        {
            var result = await _userRepository.UpdateAsync(id, updateUserDto);
            return Ok(result == true ? $"{id} number is updated" : $"{id}number is not found!!");
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _userRepository.DeleteAsync(id);
            return Ok(result == true ? $"{id} number is deleted" : $"{id}number is not found!!");
        }

    }
}