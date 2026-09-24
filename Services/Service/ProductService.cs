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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IImageService imageService, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _imageService = imageService;
            _mapper = mapper;
        }
        public async Task<AdminProductResponseDto?> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return null;
            var response= _mapper.Map<AdminProductResponseDto>(product);
            response.Images= await _imageService.GetImagesAsync("Product",product.Id);
            return response;
        }
        public async Task<List<AdminProductResponseDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var productLst= _mapper.Map<List<AdminProductResponseDto>>(products);
            foreach (var product in productLst)
            {
             product.Images = await _imageService.GetImagesAsync("Product",product.Id);  
            }
            return productLst;

        }
        //addProduct for admin
        public async Task<AdminProductResponseDto?> CreateAsync(CreateProductDto dto)
        {
            // 1. بررسی وجود Category
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);

            if (category == null)
                return null;

            // 2. تبدیل DTO به Entity
            var product = _mapper.Map<Product>(dto);

            // 3. تنظیم اطلاعاتی که Client نباید تعیین کند
            product.CreateAt = DateTime.UtcNow;
            product.CreateAt = DateTime.UtcNow;
            product.Available = product.Quantity > 0;

            // 4. ذخیره
            await _productRepository.Insert(product);
            await _productRepository.SaveChangesAsync();
            // 5. ذخیره تصاویر
            if (dto.Images != null && dto.Images.Count > 0)
            {
                int priority = 0;
                foreach (var image in dto.Images)
                {
                    await _imageService.SaveAsync(image, "Product", product.Id, priority);
                    priority++;
                }
            }
            // 6. تبدیل Entity به Response DTO
            return _mapper.Map<AdminProductResponseDto>(product);
        }
        public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
        {
            // 1. پیدا کردن Product
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return false;

            // 2. بررسی Category
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);

            if (category == null)
                return false;

            // 3. تغییر اطلاعات Product
            _mapper.Map(dto, product);
            // 4.Business Rule
            product.Available = product.Quantity > 0;
            product.UpdateDate = DateTime.UtcNow;

            // 5. ذخیره تغییرات Product
            _productRepository.Update(product);
            var result = await _productRepository.SaveChangesAsync();
            if (!result)
                return false;
            // 6. اگر Image ارسال نشده، به تصاویر دست نزن
            if (dto.Images == null || dto.Images.Count == 0)
                return true;
            // 7. اگر Add == false
            // تصاویر قبلی حذف شوند
            if (!dto.AddImage)
            {
                await _imageService.DeleteAsync("Product",product.Id);
            }
            // 8. اضافه کردن تصاویر جدید
            int priority = 0;

            foreach (var image in dto.Images)
            {
                await _imageService.SaveAsync(image,"Product",product.Id,priority);
                priority++;
            }

            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return false;

            _productRepository.Delete(product);
            await _productRepository.SaveChangesAsync();
            await _imageService.DeleteAsync("Product",product.Id);
            return true;
        }
    }
}