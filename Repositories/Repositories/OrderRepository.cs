using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories
{
    public class OrderRepository:BaseRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        public OrderRepository(AppDbContext context):base(context)
        {
            _appDbContext = context;
        }

    }
}