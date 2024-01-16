using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Models;

namespace shoppingapi2.Repositories
{
    public interface ICatogoryRepository
    {
        /// <summary>
        /// use this function to get all of categories from database
        /// </summary>
        /// <returns>List<Catogory></returns>
        Task<List<Catogory>?> GetAllAsync();
        /// <summary>
        /// use this function to Add a category to database
        /// </summary>
        /// <param name="addCatogoryDto"></param>
        /// <returns>Catogory</returns>
        Task<Catogory> AddAsync(CatogoryDto addCatogoryDto);
        /// <summary>
        /// use this function to get category by id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Catogory</returns>
        Task<Catogory?> GetByIdAsync(int id);
        /// <summary>
        /// use this function to Update category in database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateCatogoryDto"></param>
        /// <returns>bool</returns>
        Task<bool> UpdateAsync(int id, CatogoryDto updateCatogoryDto);
        /// <summary>
        /// use this function to remove the category from database by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        Task<bool> DeleteAsync(int id);


        
    }
}