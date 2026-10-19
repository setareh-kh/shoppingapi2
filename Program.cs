using Hangfire;
using Microsoft.Extensions.FileProviders;
using shoppingapi2.Installer;
using shoppingapi2.Jobs;
using shoppingapi2.Setting;
var builder = WebApplication.CreateBuilder(args);

builder.Services.InstallServicesInAssembly(builder.Configuration);

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    DashboardTitle = "shoppingapi2 Dashboard",
});

app.UseStaticFiles(new StaticFileOptions{
    FileProvider=new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"Assets")),RequestPath="/Assets"
});

app.UseMiddleware<SimpleMiddleware>();
app.UseMiddleware<JwtMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
var timezone = new RecurringJobOptions
{
    TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Tehran")
};

RecurringJob.AddOrUpdate("BirthdayEmail","critical",
    () => Console.WriteLine("Generating every 10 minutes report..."),
    "*/10 * * * *",
    timezone
);
RecurringJob.AddOrUpdate<IterativeJobs>("WriteHello","critical",j => j.WriteHello(),
    "* * * * *",
    timezone
);
app.Run();
