using AutoMapper;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Models;
using shoppingapi2.Repositories;

namespace shoppingapi2.Services.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICatogoryRepository _catogoryRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, ICatogoryRepository catogoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _catogoryRepository = catogoryRepository;
            _mapper = mapper;
        }
        public async Task<AdminProductResponseDto?> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return null;

            return _mapper.Map<AdminProductResponseDto>(product);
        }
        public async Task<List<AdminProductResponseDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();

            return _mapper.Map<List<AdminProductResponseDto>>(products);
        }
        //addProduct for admin
        public async Task<AdminProductResponseDto?> CreateAsync(CreateProductDto dto)
        {
            // 1. بررسی وجود Category
            var category = await _catogoryRepository.GetByIdAsync(dto.CatogoryId);

            if (category == null)
                return null;

            // 2. تبدیل DTO به Entity
            var product = _mapper.Map<Product>(dto);

            // 3. تنظیم اطلاعاتی که Client نباید تعیین کند
            product.CreateAt = DateTime.UtcNow;

            // 4. ذخیره
            await _productRepository.Insert(product);
            await _productRepository.SaveChangesAsync();
            // 5. تبدیل Entity به Response DTO
            return _mapper.Map<AdminProductResponseDto>(product);
        }
        public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
        {
            // 1. پیدا کردن Product
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return false;

            // 2. بررسی Category
            var category = await _catogoryRepository.GetByIdAsync(dto.CatogoryId);

            if (category == null)
                return false;

            // 3. تغییر اطلاعات Product
            _mapper.Map(dto, product);
            // 4. ثبت تغییر
            _productRepository.Update(product);
            //آپدیت مربوط به عکس ها 
            await _productRepository.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return false;

            _productRepository.Delete(product);
            await _productRepository.SaveChangesAsync();

            return true;
        }


    }
}