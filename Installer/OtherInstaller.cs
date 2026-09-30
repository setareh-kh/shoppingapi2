using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using shoppingapi2.Models;

namespace shoppingapi2.Installer;

public class OtherInstaller : IInstaller
{
    public void InstallServices(IConfiguration configuration, IServiceCollection services)
    {
        // Add services to the container.
       services.AddControllers().AddNewtonsoftJson(s =>
        {
            s.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //s.SerializerSettings.DateFormatString=""
        });

        // Learn more about configuring Swagger(swashbuckle)/OpenAPI 
       services.AddEndpointsApiExplorer();
       services.AddSwaggerGen();
       
        //install Automapper services
       services.AddAutoMapper(typeof(Program));
    }
}