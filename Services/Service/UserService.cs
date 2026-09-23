using AutoMapper;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Models;
using shoppingapi2.Repositories;


namespace shoppingapi2.Services.Service
{
   public class UserService : IUserService
   {
      private readonly IUserRepository _userRepository;
      private readonly IMapper _mapper;
      public UserService(IUserRepository userRepository, IMapper mapper)
      {
         _userRepository = userRepository;
         _mapper = mapper;
      }
      public async Task<UserUserResponseDto?> GetByIdAsync(int id)
      {
         var user = await _userRepository.GetByIdAsync(id);
         if (user == null)
            return null;
         return _mapper.Map<UserUserResponseDto>(user);
      }
      public async Task<List<UserUserResponseDto>> GetAllAsync()
      {
         var users = await _userRepository.GetAllAsync();
         return _mapper.Map<List<UserUserResponseDto>>(users);
         
      }

      public async Task<AdminUserResponseDto?> GetByMobileAsync(string mobile)
      {
         var user = await _userRepository.FindAsync(x => x.Mobile == mobile);
         if (user == null)
            return null;
         return _mapper.Map<AdminUserResponseDto>(user);
         
      }

      public async Task<UserUserResponseDto?> CreateAsync(CreateUserDto dto)
      {
         var existingUser = await _userRepository.FindAsync(
             x => x.Mobile == dto.Mobile);

         if (existingUser != null)
            return null;
         var user = _mapper.Map<User>(dto);
         user.CreateAt = DateTime.UtcNow;
         await _userRepository.InsertAsync(user);

         return _mapper.Map<UserUserResponseDto>(user);
      }

      public async Task<bool> UpdateAsync(int id, UpdateUserDto dto)
      {
         var user = await _userRepository.GetByIdAsync(id);

         if (user == null)
            return false;
         user.Name = dto.Name;
         user.Mobile = dto.Mobile;
         _userRepository.Update(user);
         await _userRepository.SaveChangesAsync();
         return true;
      }

      public async Task<bool> DeleteAsync(int id)
      {
         var user = await _userRepository.GetByIdAsync(id);

         if (user == null)
            return false;

         await _userRepository.DeleteAsync(user);

         return true;
      }
   }

}

