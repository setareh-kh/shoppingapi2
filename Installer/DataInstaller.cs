using Hangfire;
using Hangfire.Redis.StackExchange;
using Microsoft.EntityFrameworkCore;
using shoppingapi2.Models;
using StackExchange.Redis;

namespace shoppingapi2.Installer;

public class DataInstaller : IInstaller
{
    public void InstallServices(IConfiguration configuration, IServiceCollection services)
    {
        //mysql connection Setting
        var myConnection = configuration.GetConnectionString("MyConnection") ?? throw new InvalidOperationException(
            "Connection string 'MySqlConnection' was not found.");
        services.AddDbContext<AppDbContext>(options => options.UseMySql(myConnection, ServerVersion.AutoDetect(myConnection)));

        var redisString = configuration.GetValue<string>("RedisConnectionStrings:Redis");
        var redis = ConnectionMultiplexer.Connect(redisString ?? "");
        services.AddHangfire(configuration =>
        {
            configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseRedisStorage(redis,new RedisStorageOptions{Prefix = "{shoppingapi2-hangfire}:", Db = 0});
        });


        services.AddHangfireServer(serverOptions =>
        {
            serverOptions.ServerName = "Hangfire.Redis server 1";
            serverOptions.WorkerCount = 2;
            serverOptions.Queues = new string[] {"default"}; // Define your queues
        });

        services.AddHangfireServer(serverOptions =>
        {
            serverOptions.ServerName = "Hangfire.Redis server 2";
            serverOptions.WorkerCount = 10;
            serverOptions.Queues = new string[] {"critical"}; // Define your queues
        });
    }
}