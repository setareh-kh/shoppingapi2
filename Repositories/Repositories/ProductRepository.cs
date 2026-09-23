using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories
{
    public class ProductRepository:BaseRepository<Product>, IProductRepository
    {
    
        public ProductRepository(AppDbContext context):base(context)
        {
        }
        

    }
}