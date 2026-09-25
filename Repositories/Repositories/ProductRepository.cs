using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories
{
    public class ProductRepository:BaseRepository<Product>, IProductRepository
    {
    
        public ProductRepository(AppDbContext context):base(context)
        {
        }
        public async Task<PaginateResponseDto<Product>> Filter(ProductFilterDto filterDto)
    {
        var query = AppDbContext.Products.AsQueryable();
        if (!string.IsNullOrWhiteSpace(filterDto.Search))
        {
            query = query.Where(x => x.Name.Contains(filterDto.Search));
        }

        if (filterDto.Active != null)
        {
            query = query.Where(x => x.Active == filterDto.Active);
        }

        if (filterDto.Available!= null)
        {
            query = query.Where(x => x.Available == filterDto.Available);
        }
        if (filterDto.CategoryId!= null)
        {
            query = query.Where(x => x.CategoryId == filterDto.CategoryId);
        }

        /*if (filterDto.Include)
        {
            query = query.Include(x => x.);
        }*/

        return await Paginate(filterDto, query);
    }
        
    }
}