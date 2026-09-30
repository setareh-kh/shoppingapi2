namespace shoppingapi2.Installer;
public interface IInstaller
{
    void InstallServices(IConfiguration configuration, IServiceCollection services);
    
}