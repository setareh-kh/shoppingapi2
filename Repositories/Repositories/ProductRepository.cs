using Microsoft.EntityFrameworkCore;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {

        public ProductRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<PaginateResponseDto<Product>> Filter(ProductFilterDto filterDto)
        {
            var query = AppDbContext.Products.AsQueryable();
            // Search
            if (!string.IsNullOrWhiteSpace(filterDto.Search))
            {
                var search = filterDto.Search.Trim();
                query = query.Where(x => x.Name.Contains(search));
            }

            // Name
            if (!string.IsNullOrWhiteSpace(filterDto.Name))
            {
                var name = filterDto.Name.Trim();
                query = query.Where(x => x.Name.Contains(name));
            }

            // Min Price
            if (filterDto.MinPrice.HasValue)
            {
                query = query.Where(x => x.Price >= filterDto.MinPrice.Value);
            }

            // Max Price
            if (filterDto.MaxPrice.HasValue)
            {
                query = query.Where(x => x.Price <= filterDto.MaxPrice.Value);
            }

            // Min Quantity
            if (filterDto.MinQuantity.HasValue)
            {
                query = query.Where(x => x.Quantity >= filterDto.MinQuantity.Value);
            }

            // Max Quantity
            if (filterDto.MaxQuantity.HasValue)
            {
                query = query.Where(x => x.Quantity <= filterDto.MaxQuantity.Value);
            }

            // Min Discount
            if (filterDto.MinDiscount.HasValue)
            {
                query = query.Where(x => x.Discount >= filterDto.MinDiscount.Value);
            }

            // Max Discount
            if (filterDto.MaxDiscount.HasValue)
            {
                query = query.Where(x => x.Discount <= filterDto.MaxDiscount.Value);
            }

            // Active
            if (filterDto.Active.HasValue)
            {
                query = query.Where(x => x.Active == filterDto.Active.Value);
            }

            // Available
            if (filterDto.Available.HasValue)
            {
                query = query.Where(x => x.Available == filterDto.Available.Value);
            }

            // Category
            if (filterDto.CategoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == filterDto.CategoryId.Value);
            }

            // Include Category
            if (filterDto.Include)
            {
                query = query.Include(x => x.Category);
            }

            return await Paginate(filterDto, query);
        }

    }
}