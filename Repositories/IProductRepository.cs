using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Models;

namespace shoppingapi2.Repositories
{
    public interface IProductRepository:IBaseRepository<Product>
    {
        Task<PaginateResponseDto<Product>> Filter(ProductFilterDto filterDto);
    }
}