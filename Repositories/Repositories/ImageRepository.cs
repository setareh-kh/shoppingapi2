using Microsoft.EntityFrameworkCore;
using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories
{
    public class ImageRepository:IImageRepository
    {
        private readonly AppDbContext _context;
        public ImageRepository(AppDbContext context)
        {
            _context = context;
        }
        private async Task<string> StoreToFileAsync(string uniqStart, string lastPath, IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName);
            var randomName = Path.GetRandomFileName();
            var fileName = $"{uniqStart}_{randomName}{ext}";
            var storeDirectore = Path.Combine(Directory.GetCurrentDirectory(), "Assets", lastPath);
            if (!Directory.Exists(storeDirectore))
                Directory.CreateDirectory(storeDirectore);
            var storeFile = Path.Combine(storeDirectore, fileName);
            if (File.Exists(storeFile))
                File.Delete(storeFile);
            using (var fs = File.Create(storeFile))
            {
                await file.CopyToAsync(fs);
            }
            return fileName;
        }
        public async Task<Image> SaveAsync(IFormFile file, string itemType, int itemId, int priority=0)
        {
            string nameStart = itemId.ToString();
            var name = await StoreToFileAsync(nameStart, $"{itemType}s", file);
            var image = new Image
            {
                Name = name,
                ItemType = itemType,
                ItemId = itemId,
                Url = $"/Assets/{itemType}s/{name}",
                Priority = priority
            };
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();
            return image;
        }
        public async Task<Image?> GetAsync(string itemType,int itemId)
        {
            var image=await _context.Images.FirstOrDefaultAsync(image=>image.ItemType==itemType && image.ItemId==itemId);
            return image;
        }
    }
}