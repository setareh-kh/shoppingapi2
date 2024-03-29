using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Models;

namespace shoppingapi2.Repositories
{
    public interface IOrderDetailsRepository
    {
        Task<List<OrderDetails>?> GetAllAsync();
        Task<OrderDetails> AddAsync(AddOrderDetailsDto addOrderDetailsDto);
        Task<OrderDetails?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, UpdateOrderDetailsDto updateOrderDetailsDto);
        Task<bool> DeleteAsync(int id);
        
    }
}