using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories
{
    public class CatogoryRepository: BaseRepository<Catogory>,ICatogoryRepository
    {
       public CatogoryRepository(AppDbContext context):base(context)
        {
        }

    }
}