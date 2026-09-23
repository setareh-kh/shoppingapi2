using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories
{
    public class CategoryRepository: BaseRepository<Category>,ICategoryRepository
    {
       public CategoryRepository(AppDbContext context):base(context)
        {
        }

    }
}