using Microsoft.EntityFrameworkCore;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context=context;
        }
        //Get all users from the database
        public async Task<List<User>> GetAllAsync() 
        {
           var users= await _context.Users.ToListAsync();
            return users;
        }
        //Add a new user to the database
        public async Task AddAsync (AddUserDto addUserDto)
        {
            
        }

    }
}