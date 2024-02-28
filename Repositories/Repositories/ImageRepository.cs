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
            string lastPath, url;
            switch (itemType)
            {
                case "Product":
                    lastPath = "Products";
                    url = "/Assets/Products";
                    break;
                case "User":
                    lastPath = "Users";
                    url = "/Assets/Users";
                    break;
                default:
                    lastPath = "Users";
                    url = "/Assets/Users";
                    break;

            }
            var name = await StoreToFileAsync(nameStart, lastPath, file);
            var image = new Image
            {
                Name = name,
                ItemType = itemType,
                ItemId = itemId,
                Url = $"{url}/{name}",
                Priority = priority
            };
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();
            return image;
        }
        //public async Task<Image> GetAsync(string itemType,int itemId)
    }
}