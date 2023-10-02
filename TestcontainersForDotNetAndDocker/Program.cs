

using Microsoft.EntityFrameworkCore;
using TestcontainersForDotNetAndDocker.Database;
using TestcontainersForDotNetAndDocker.Repositories;
using TestcontainersForDotNetAndDocker.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
 builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
            
builder.Services.AddScoped<ICatRepository, CatRepository>();
builder.Services.AddScoped<ICatService, CatService>();
builder.Services.AddScoped<DatabaseInitializer, DatabaseInitializer>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetService<DatabaseInitializer>();
await context.InitializeAsync();

app.MapControllers();

app.Run();
public partial class Program { }