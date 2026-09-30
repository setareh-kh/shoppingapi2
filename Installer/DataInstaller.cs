using Microsoft.EntityFrameworkCore;
using shoppingapi2.Models;

namespace shoppingapi2.Installer;

public class DataInstaller : IInstaller
{
    public void InstallServices(IConfiguration configuration, IServiceCollection services)
    {
        //mysql connection Setting
        var myConnection = configuration.GetConnectionString("MyConnection") ?? throw new InvalidOperationException(
            "Connection string 'MySqlConnection' was not found.");
        services.AddDbContext<AppDbContext>(options => options.UseMySql(myConnection, ServerVersion.AutoDetect(myConnection)));
    }
}