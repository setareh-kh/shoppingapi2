using shoppingapi2.Models;

namespace shoppingapi2.Repositories
{
    public interface IImageRepository
    {
        Task<Image> SaveAsync(IFormFile file, string itemType, int itemId, int priority=0);
        Task<Image?> GetAsync(string itemType,int itemId);
        Task<List<Image>?> GetImagesAsync(string itemType, int itemId);
        Task<bool> DeleteAsync(string itemType, int itemid);
        
    }
}