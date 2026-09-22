using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories
{
    public class OrderDetailsRepository: BaseRepository<OrderDetails>, IOrderDetailsRepository
    {
        private readonly AppDbContext _appDbContext;
        public OrderDetailsRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }
    }
}