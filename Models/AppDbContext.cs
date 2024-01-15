using Microsoft.EntityFrameworkCore;

namespace shoppingapi2.Models
{
    public class AppDbContext:DbContext   
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<User> Users {get; set;}
        public DbSet<Product> Products {get; set;}
        public DbSet<Catogory> Catogories {get; set;}
    }    
}