using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories
{
    public class TokenRepository : BaseRepository<Token>, ITokenRepository 
    {
       public TokenRepository (AppDbContext context):base(context)
        {
        }

    }
}