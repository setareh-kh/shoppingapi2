using AutoMapper;
using Microsoft.EntityFrameworkCore;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories
{
    public class ProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ProductRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<Product>?> GetAllAsync()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }
        public async Task<Product> AddAsync(AddProductDto addProductDto)
        {
            Product product = _mapper.Map<Product>(addProductDto);
            product.CreateAt=DateTime.Now;
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<Product?> GetByIdAsync(int id)
        {
            Product? product = await _context.Products.FindAsync(id);
            return product;
        }
        public async Task<bool> UpdateAsync(int id, UpdateProductDto updateProductDto)
        {
            Product? product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _mapper.Map(updateProductDto, product);
                product.UpdateDate=DateTime.Now;
                await _context.SaveChangesAsync();
                return true;
            }
            else return false;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            Product? product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            else return false;
        }

    }
}