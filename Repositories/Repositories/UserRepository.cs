using AutoMapper;
using Microsoft.EntityFrameworkCore;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public UserRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<User>?> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }
        public async Task<User> AddAsync(AddUserDto addUserDto)
        {
            User user = _mapper.Map<User>(addUserDto);
            user.CreateAt = DateTime.Now;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            /*
            var extension = Path.GetExtension(addUserDto.File.FileName);
            var name = Path.GetRandomFileName();
            var fileName = $"{name}{extension}";
            var storeDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Users", user.Id.ToString());
            if (!Directory.Exists(storeDirectory))
                Directory.CreateDirectory(storeDirectory);
            var storeFile = Path.Combine(storeDirectory, fileName);
            if (File.Exists(storeFile))
                File.Delete(storeFile);
            using (var fs = File.Create(storeFile))
            {
                await addUserDto.File.CopyToAsync(fs);
            }*/
            return user;
        }
        public async Task<User?> GetByIdAsync(int id)
        {
            User? user = await _context.Users.FindAsync(id);
            return user;
        }
        public async Task<bool> UpdateAsync(int id, UpdateUserDto updateUserDto)
        {
            User? user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _mapper.Map(updateUserDto, user);
                await _context.SaveChangesAsync();
                return true;
            }
            else return false;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            User? user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            else return false;
        }
        public async Task<User?> LoginAsync(LoginUserDto loginUserDto)
        {
            User? user = await _context.Users.Where(u=>u.Mobile==loginUserDto.Mobile && u.Password==loginUserDto.Password).FirstOrDefaultAsync();
            return user;
        }


    }
}