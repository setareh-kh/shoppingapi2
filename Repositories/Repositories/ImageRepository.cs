using Microsoft.EntityFrameworkCore;
using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories
{
    public class ImageRepository : IImageRepository
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
            var storeDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Assets", lastPath);
            if (!Directory.Exists(storeDirectory))
                Directory.CreateDirectory(storeDirectory);
            var storeFile = Path.Combine(storeDirectory, fileName);
            if (File.Exists(storeFile))
                File.Delete(storeFile);
            using (var fs = File.Create(storeFile))
            {
                await file.CopyToAsync(fs);
            }
            return fileName;
        }
        public async Task<Image> SaveAsync(IFormFile file, string itemType, int itemId, int priority = 0)
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
        public async Task<Image?> GetAsync(string itemType, int itemId)
        {
            return await _context.Images.FirstOrDefaultAsync(image => image.ItemType == itemType && image.ItemId == itemId);
        }
        public async Task<List<Image>?> GetImagesAsync(string itemType, int itemId)
        {
            return await _context.Images.Where(x => x.ItemType == itemType && x.ItemId == itemId).ToListAsync();
        }
        public async Task<bool> DeleteAsync(string itemType, int itemid)
        {
            var imgs = await _context.Images.Where(x => x.ItemType == itemType && x.ItemId == itemid).ToListAsync();
            if (imgs == null) return false;
            foreach (var image in imgs)
            {
                var dltFile = $"{Directory.GetCurrentDirectory()}\\Assets\\{itemType}s\\{image.Name}";
                //Console.WriteLine(Directory.GetCurrentDirectory());
                //Console.WriteLine(image.Url);
                //Console.WriteLine(dltFile);
                System.IO.File.Delete(dltFile);
                _context.Images.Remove(image);
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}