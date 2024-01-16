using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Models;

namespace shoppingapi2.Repositories
{
    public interface IProductRepository
    {
        /// <summary>
        /// use this function to get all of products from database
        /// </summary>
        /// <returns>List<Product></returns>
        Task<List<Product>?> GetAllAsync();
        /// <summary>
        /// use this function to Add a product to database
        /// </summary>
        /// <param name="addProductDto"></param>
        /// <returns>Product</returns>
        Task<Product> AddAsync(AddProductDto addProductDto);
        /// <summary>
        /// use this function to get product by id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product</returns>
        Task<Product?> GetByIdAsync(int id);
        /// <summary>
        /// use this function to Update product in database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateProductDto"></param>
        /// <returns>bool</returns>
        Task<bool> UpdateAsync(int id, UpdateProductDto updateProductDto);
        /// <summary>
        /// use this function to remove the product from database by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        Task<bool> DeleteAsync(int id);
        
    }
}