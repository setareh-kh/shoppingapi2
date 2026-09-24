using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Models;

namespace shoppingapi2.Services;

public interface IImageService
{
    Task<Image?> SaveAsync(IFormFile file, string itemType, int itemId, int priority = 0);
    Task<Image?> GetAsync(string itemType, int itemId);
    Task<List<Image>> GetImagesAsync(string itemType, int itemId);
    Task<bool> DeleteAsync(string itemType,int itemId);
}