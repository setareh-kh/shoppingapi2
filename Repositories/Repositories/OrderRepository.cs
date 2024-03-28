using AutoMapper;
using Microsoft.EntityFrameworkCore;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public OrderRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<Order>?> GetAllAsync()
        {
            var orders = await _context.Orders.Include(p => p.User).ToListAsync();
            return orders;
        }
        public async Task<Order> AddAsync(AddOrderDto addOrderDto)
        {
            Order order = _mapper.Map<Order>(addOrderDto);
            order.CreateAt = DateTime.Now;
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }
        public async Task<Order?> GetByIdAsync(int id)
        {
            Order? order = await _context.Orders.FindAsync(id);
            return order;
        }
        public async Task<bool> UpdateAsync(int id, UpdateOrderDto updateOrderDto)
        {
            Order? order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _mapper.Map(updateOrderDto, order);
                order.UpdateDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            Order? order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Remove(order);
                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

    }
}