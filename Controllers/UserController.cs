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
        public UserController(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository=userRepository;
            _mapper=mapper;
        }
        [HttpPost]
        [Route("Add")] 
        public async Task<IActionResult> AddAsync([FromBody] AddUserDto addUserDto)
        {
            var user=await _userRepository.AddAsync(addUserDto);
            return Ok(_mapper.Map<UserUserDto>(user));
        }
        [HttpGet]
        [Route("Get")] 
        public async Task<IActionResult> GetById(int id)
        {
            
        }
    }
}