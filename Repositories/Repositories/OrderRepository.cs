using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories
{
    public class OrderRepository:BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context):base(context)
        {
        }

    }
}