using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Models;

namespace shoppingapi2.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        ///  use this function to get all of users from database
        /// </summary>
        /// <returns>List<User></returns>
        Task<List<User>?> GetAllAsync();
        /// <summary>
        /// use this function to Add a user to database
        /// </summary>
        /// <param name="addUserDto"></param>
        /// <returns>User</returns>
        Task<User> AddAsync(AddUserDto addUserDto);
        /// <summary>
        /// use this function to get user by id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User</returns>
        Task<User?> GetByIdAsync(int id);
        /// <summary>
        ///  use this function to Update user in database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateUserDto"></param>
        /// <returns>bool</returns>
        Task<bool> UpdateAsync(int id, UpdateUserDto updateUserDto);
        /// <summary>
        /// use this function to remove the user from database by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        Task<bool> DeleteAsync(int id);

    }
}