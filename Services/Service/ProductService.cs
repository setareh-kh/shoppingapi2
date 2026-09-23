using AutoMapper;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Models;
using shoppingapi2.Repositories;

namespace shoppingapi2.Services.Service
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IImageRepository imageRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _imageRepository = imageRepository;
            _mapper = mapper;
        }
        //addProduct for admin
        public async Task<AdminProductResponseDto> AddAsync(AddProductDto addProductDto)
        {
            var product = _mapper.Map<Product>(addProductDto);
            await _productRepository.InsertAsync(product);
            var response = _mapper.Map<AdminProductResponseDto>(product);
            List<Image> imgLst = new();
            foreach (var item in addProductDto.Files)
            {
                imgLst.Add(await _imageRepository.SaveAsync(item, "Product", product.Id));
            }
            response.Images = imgLst;
            return response ;
        }
        /*
            CreateProduct
GetProduct
GetProducts
UpdateProduct
DeleteProduct
GetProductsByCategory
        */
        public async Task<List<Product>> GetByCategoryAsync(int categoryId)
{
    return await _productRepository
        .WhereAsync(x => x.CatogoryId == categoryId);
}
        /*
        Product وجود دارد؟
       ↓
Active است؟
       ↓
Available است؟
       ↓
Quantity کافی است؟
       ↓
قیمت فعلی چقدر است؟
       ↓
Discount چقدر است؟
        */



    }
}