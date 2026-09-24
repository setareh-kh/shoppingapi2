using AutoMapper;
using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Models;
using shoppingapi2.Repositories;

namespace shoppingapi2.Services.Service;

public class ImageService : IImageService
{
    private readonly IImageRepository _imageRepository;
    private readonly IMapper _mapper;

    public ImageService(IImageRepository imageRepository, IMapper mapper)
    {
        _imageRepository = imageRepository;
        _mapper = mapper;
    }

    public async Task<Image?> SaveAsync(IFormFile file, string itemType, int itemId, int priority = 0)
    {
        if (file == null || file.Length == 0)
            return null;

        var image = await _imageRepository.SaveAsync(file, itemType, itemId, priority);
        if (image == null)
            return null;
        return image;
    }

    public async Task<Image?> GetAsync(string itemType, int itemId)
    {
        var image = await _imageRepository.GetAsync(itemType, itemId);
        if (image == null)
            return null;
        return image;
    }

    public async Task<List<Image>> GetImagesAsync(string itemType, int itemId)
    {
         return await _imageRepository.GetImagesAsync(itemType, itemId);
         
    }

    public async Task<bool> DeleteAsync(string itemType,int itemId)
    {
        return await _imageRepository.DeleteAsync(itemType,itemId);
    }
}