using Microsoft.EntityFrameworkCore;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<PaginateResponseDto<Order>> Filter(OrderFilterDto filterDto)
        {
            var query = AppDbContext.Orders.AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(filterDto.Search))
            {
                var search = filterDto.Search.Trim();
                query = query.Where(x => x.User.Name.Contains(search) ||
                    x.User.Mobile.Contains(search));
            }

            // User
            if (filterDto.UserId.HasValue)
            {
                query = query.Where(x => x.UserId == filterDto.UserId.Value);
            }

            // Min TotalPrice
            if (filterDto.MinTotalPrice.HasValue)
            {
                query = query.Where(x => x.TotalPrice >= filterDto.MinTotalPrice.Value);
            }

            // Max TotalPrice
            if (filterDto.MaxTotalPrice.HasValue)
            {
                query = query.Where(x => x.TotalPrice <= filterDto.MaxTotalPrice.Value);
            }

            // From CreateAt
            if (filterDto.FromDate.HasValue)
            {
                query = query.Where(x => x.CreateAt >= filterDto.FromDate.Value);
            }

            // To CreateAt
            if (filterDto.ToDate.HasValue)
            {
                var toDate = filterDto.ToDate.Value.Date.AddDays(1);
                query = query.Where(x => x.CreateAt < toDate);
            }

            // From UpdateDate
            if (filterDto.FromUpdateDate.HasValue)
            {
                query = query.Where(x =>
                    x.UpdateDate >= filterDto.FromUpdateDate.Value);
            }

            // To UpdateDate
            if (filterDto.ToUpdateDate.HasValue)
            {
                var toDate = filterDto.ToUpdateDate.Value.Date.AddDays(1);
                query = query.Where(x =>x.UpdateDate < toDate);
            }
            if (filterDto.Include)
            {
                query = query
                    .Include(x => x.User)
                    .Include(x => x.OrderDetails).ThenInclude(x => x.Product);
            }

            return await Paginate(filterDto, query);
        }
    }
}