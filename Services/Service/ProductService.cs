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


    }
}