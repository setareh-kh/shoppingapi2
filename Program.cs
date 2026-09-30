using Microsoft.Extensions.FileProviders;
using shoppingapi2.Installer;
var builder = WebApplication.CreateBuilder(args);

builder.Services.InstallServicesInAssembly(builder.Configuration);

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles(new StaticFileOptions{
    FileProvider=new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"Assets")),RequestPath="/Assets"
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
