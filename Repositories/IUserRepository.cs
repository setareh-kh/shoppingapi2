using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Models;

namespace shoppingapi2.Repositories
{
    public interface IUserRepository:IBaseRepository<User>
    {
       Task<PaginateResponseDto<User>> Filter(UserFilterDto filter);

    }
}