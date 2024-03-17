using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;
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
                var Response = _mapper.Map<AdminUserResponseDto>(user);
                Response.ImageProfile = await _imageRepository.GetAsync("User", user.Id);
                return Ok(Response);
            }

            return Ok("login faild");
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddAsync([FromForm] AddUserDto addUserDto)
        {
            var user = await _userRepository.AddAsync(addUserDto);
            var Response = _mapper.Map<AdminUserResponseDto>(user);
            Response.ImageProfile = await _imageRepository.SaveAsync(addUserDto.File, "User", user.Id);
            return Ok(Response);
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            if (users != null)
            {
                var responses = users.Select(x => _mapper.Map<AdminUserResponseDto>(x)).ToList();
                foreach (var user in responses)
                {
                    user.ImageProfile = await _imageRepository.GetAsync("User", user.Id);
                }
                return Ok(responses);
            }
            else
                return Ok(users?.Count > 0 ? users.Select(x => _mapper.Map<AdminUserResponseDto>(x)) : "No Any exisit user");
        }
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                var Response = _mapper.Map<UserUserResponseDto>(user);
                Response.ImageProfile = await _imageRepository.GetAsync("User", user.Id);
                return Ok(Response);
            }
            else
                return Ok($"user by {id} number is not found!!");

        }
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] UpdateUserDto updateUserDto)
        {
            var userResult = await _userRepository.UpdateAsync(id, updateUserDto);
            if (!userResult)
                return Ok($"{id}number is not found!!");
            var imgResult = await _imageRepository.UpdateAsync(updateUserDto.File, "User", id, isAdded: false);
            if (imgResult)
                return Ok($"successfully updated the user and his Image Profile");
            else
                return Ok("Error when updating image profile");
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var userResult = await _userRepository.DeleteAsync(id);
            if (!userResult)
                return Ok($"{id} number is not found!");

            var imgResult = await _imageRepository.DeleteAsync("User", id);
            if (imgResult)
                return Ok("Successfuly deleted the user and his Image Profile");
            else
                return Ok("Failed to delete Image Profile");

        }

    }
}