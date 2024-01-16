using AutoMapper;
using Microsoft.EntityFrameworkCore;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories
{
    public class CatogoryRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CatogoryRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<Catogory>?> GetAllAsync()
        {
            var catogories = await _context.Catogories.ToListAsync();
            return catogories;
        }
        public async Task<Catogory> AddAsync(CatogoryDto addCatogoryDto)
        {
            Catogory catogory = _mapper.Map<Catogory>(addCatogoryDto);
            await _context.Catogories.AddAsync(catogory);
            await _context.SaveChangesAsync();
            return catogory;
        }
        public async Task<Catogory?> GetByIdAsync(int id)
        {
            Catogory? catogory = await _context.Catogories.FindAsync(id);
            return catogory;
        }
        public async Task<bool> UpdateAsync(int id, CatogoryDto updateCatogoryDto)
        {
            Catogory? catogory = await _context.Catogories.FindAsync(id);
            if (catogory != null)
            {
                _mapper.Map(updateCatogoryDto, catogory);
                await _context.SaveChangesAsync();
                return true;
            }
            else return false;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            Catogory? catogory = await _context.Catogories.FindAsync(id);
            if (catogory != null)
            {
                _context.Remove(catogory);
                await _context.SaveChangesAsync();
                return true;
            }
            else return false;
        }

    }
}