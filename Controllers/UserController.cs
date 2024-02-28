using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Models;
using shoppingapi2.Repositories;

namespace shoppingapi2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;
        private int currentUserType = 0;
        public UserController(IUserRepository userRepository, IMapper mapper, IImageRepository imageRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _imageRepository = imageRepository;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserDto loginUserDto)
        {
            var user = await _userRepository.LoginAsync(loginUserDto);
            if (user != null)
            {
                currentUserType = user.Type;
                return Ok(GetResponseDto(user));
            }
            else return Ok("login faild");

        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddAsync([FromForm] AddUserDto addUserDto)
        {
            var user = await _userRepository.AddAsync(addUserDto);
            var Response=GetResponseDto(user);
            Response.ImageProfile= await _imageRepository.SaveAsync(addUserDto.File, "User", user.Id);
            return Ok(Response);
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users?.Count > 0 ? users.Select(x => GetResponseDto(x)) : "No Any exisit user");
        }
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                var Response= GetResponseDto(user);
                Response.ImageProfile=await _imageRepository.GetAsync("user", user.Id);
            }
    
            return Ok(user != null ? Response : $"{id}number is not found!!");

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

        private dynamic GetResponseDto(User user)
        {
            if (currentUserType == 1)
                return _mapper.Map<UserUserResponseDto>(user);

            return _mapper.Map<AdminUserResponseDto>(user);

        }
    }
}