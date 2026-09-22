using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories
{
    public class CatogoryRepository: BaseRepository<Catogory>,ICatogoryRepository
    {
       protected readonly AppDbContext appDbContext;
       public CatogoryRepository(AppDbContext context):base(context)
        {
            appDbContext=context;
        }

    }
}