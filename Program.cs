using Microsoft.EntityFrameworkCore;
using shoppingapi2.Models;
using shoppingapi2.Repositories;
using shoppingapi2.Repositories.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//mysql connection Setting
var myConnection=builder.Configuration.GetConnectionString("MyConnection");
builder.Services.AddDbContext<AppDbContext>(options=>options.UseMySql(myConnection,ServerVersion.AutoDetect(myConnection)));
//install Automapper services
builder.Services.AddAutoMapper(typeof(Program));
//install Irepositor and repository as services
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<ICatogoryRepository,CatogoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
