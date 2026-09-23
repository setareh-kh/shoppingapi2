using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories
{
    public class OrderDetailsRepository : BaseRepository<OrderDetails>, IOrderDetailsRepository
    {
        public OrderDetailsRepository(AppDbContext context) : base(context)
        {

        }
    }
}