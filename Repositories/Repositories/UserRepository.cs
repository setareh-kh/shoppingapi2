using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories
{
    public class UserRepository :BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context):base(context)
        {
        
        } 

    }
}