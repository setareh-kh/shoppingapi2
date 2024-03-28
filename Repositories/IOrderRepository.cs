using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Models;

namespace shoppingapi2.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>?> GetAllAsync();
        Task<Order> AddAsync(AddOrderDto addOrderDto);
        Task<Order?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, UpdateOrderDto updateOrderDto);
        Task<bool> DeleteAsync(int id);

    }
}