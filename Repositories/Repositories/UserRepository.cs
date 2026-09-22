using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories
{
    public class UserRepository :BaseRepository<User>, IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        public UserRepository(AppDbContext context):base(context)
        {
           _appDbContext = context;
        } 

    }
}