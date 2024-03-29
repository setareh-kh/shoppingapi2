using AutoMapper;
using Microsoft.EntityFrameworkCore;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public OrderDetailsRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<OrderDetails>?> GetAllAsync()
        {
            List<OrderDetails>? orderDetails = await _context.OrderDetails.Include(oD =>oD.Product).Include(oD =>oD.Order).ToListAsync();
            return orderDetails;
        }
        public async Task<OrderDetails> AddAsync(AddOrderDetailsDto addOrderDetailsDto)
        {
            OrderDetails orderDetails = _mapper.Map<OrderDetails>(addOrderDetailsDto);
            orderDetails.CreateAt = DateTime.Now;
            await _context.OrderDetails.AddAsync(orderDetails);
            await _context.SaveChangesAsync();
            return orderDetails;
        }
        public async Task<OrderDetails?> GetByIdAsync(int id)
        {
            OrderDetails? orderDetails = await _context.OrderDetails.FindAsync(id);
            return orderDetails;
        }
        public async Task<bool> UpdateAsync(int id, UpdateOrderDetailsDto updateOrderDetailsDto)
        {
            OrderDetails? orderDetails = await _context.OrderDetails.FindAsync(id);
            if (orderDetails != null)
            {
                _mapper.Map(updateOrderDetailsDto, orderDetails);
                orderDetails.UpdateDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public async Task<bool> DeleteAsync(int id)
        {
           OrderDetails? orderDetails = await _context.OrderDetails.FindAsync(id);
            if (orderDetails != null)
            {
                _context.Remove(orderDetails);
                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

    }
}