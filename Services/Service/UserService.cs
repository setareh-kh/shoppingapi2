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
      private readonly IImageService _imageService;
      private readonly IMapper _mapper;
      public UserService(IUserRepository userRepository, IMapper mapper, IImageService imageService)
      {
         _userRepository = userRepository;
         _imageService = imageService;
         _mapper = mapper;
      }
      public async Task<UserUserResponseDto?> GetByIdAsync(int id)
      {
         var user = await _userRepository.GetByIdAsync(id);
         if (user == null)
            return null;
         var response = _mapper.Map<UserUserResponseDto>(user);
         response.ImageProfile = await _imageService.GetAsync("User", user.Id);
         return response;
      }
      public async Task<List<UserUserResponseDto>> GetAllAsync()
      {
         var users = await _userRepository.GetAllAsync();
         var responses = _mapper.Map<List<UserUserResponseDto>>(users);
         foreach (var (usr, resp) in users.Zip(responses))
         {
            resp.ImageProfile = await _imageService.GetAsync("User", usr.Id);

         }
         return responses;
      }
      public async Task<AdminUserResponseDto?> GetByMobileAsync(string mobile)
      {
         var user = await _userRepository.FindAsync(x => x.Mobile == mobile);
         if (user == null)
            return null;

         var response = _mapper.Map<AdminUserResponseDto>(user);
         response.ImageProfile = await _imageService.GetAsync("User", user.Id);
         return response;

      }
      public async Task<UserUserResponseDto?> CreateAsync(CreateUserDto dto)
      {
         var existingUser = await _userRepository.FindAsync(x => x.Mobile == dto.Mobile);
         if (existingUser != null)
            return null;
         var user = _mapper.Map<User>(dto);
         user.CreateAt = DateTime.UtcNow;
         await _userRepository.InsertAsync(user);
         var response = _mapper.Map<UserUserResponseDto>(user);
         if (dto.Image != null)
         {
            await _imageService.SaveAsync(dto.Image, "User", user.Id, 1);
            response.ImageProfile = await _imageService.GetAsync("User", user.Id);

         }
         return response;
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
         if(dto.Image!=null)
            if(dto.AddImage)
               await _imageService.SaveAsync(dto.Image, "User", user.Id, 1);
            else
            {
             await  _imageService.DeleteAsync("User", user.Id);
             await _imageService.SaveAsync(dto.Image, "User", user.Id, 1);
            }
         return true;
      }

      public async Task<bool> DeleteAsync(int id)
      {
         var user = await _userRepository.GetByIdAsync(id);

         if (user == null)
            return false;
         
         await _userRepository.DeleteAsync(user);
         await _imageService.DeleteAsync("User", user.Id);
         return true;
      }
   }

}

