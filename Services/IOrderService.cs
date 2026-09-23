using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;

namespace shoppingapi2.Services;
public interface IOrderService
{
    Task<OrderResponseDto?> CreateAsync(CreateOrderDto dto);
}