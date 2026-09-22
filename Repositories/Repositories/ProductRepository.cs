using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories
{
    public class ProductRepository:BaseRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProductRepository(AppDbContext context):base(context)
        {
            _appDbContext = context;
        }
        

    }
}