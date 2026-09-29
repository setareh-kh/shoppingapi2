using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {

        }
        public async Task<PaginateResponseDto<User>> Filter(UserFilterDto filter)
        {
            var query = AppDbContext.Users.AsQueryable();
            // Search
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                var search = filter.Search.Trim();
                query = query.Where(x => x.Name.Contains(search) || x.Mobile.Contains(search));
            }

            // Name
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                var name = filter.Name.Trim();
                query = query.Where(x => x.Name.Contains(name));
            }

            // Mobile
            if (!string.IsNullOrWhiteSpace(filter.Mobile))
            {
                var mobile = filter.Mobile.Trim();
                query = query.Where(x => x.Mobile.Contains(mobile));
            }

            // Type
            if (filter.Type.HasValue)
            {
                query = query.Where(x => x.Type == filter.Type.Value);
            }

            // FromDate
            if (filter.FromDate.HasValue)
            {
                query = query.Where(x => x.CreateAt >= filter.FromDate.Value);
            }

            // ToDate
            if (filter.ToDate.HasValue)
            {
                query = query.Where(x => x.CreateAt <= filter.ToDate.Value);
            }

            return await Paginate(filter, query);


        }

    }
}