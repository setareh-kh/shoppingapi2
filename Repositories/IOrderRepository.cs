using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Models;

namespace shoppingapi2.Repositories
{
    public interface IOrderRepository:IBaseRepository<Order>
    {
        Task<PaginateResponseDto<Order>> Filter(OrderFilterDto filterDto);
    }
}