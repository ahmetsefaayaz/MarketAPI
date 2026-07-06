using FluentValidation;
using FluentValidation.AspNetCore;
using MarketAPI.Application.Dtos.Validators;
using MarketAPI.Application.Interfaces.IOrder;
using MarketAPI.Application.Interfaces.IProduct;
using MarketAPI.Application.Interfaces.IUnitOfWork;
using MarketAPI.Application.Services;
using MarketAPI.Persistence.DbContexts;
using MarketAPI.Persistence.Repositories;
using MarketAPI.Presentation.Middlewares;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/market-log-.txt",  rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddDbContext<MarketAPIDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateProductValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


var app = builder.Build();
app.UseMiddleware<GlobalExceptionMiddleware>();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<MarketAPIDbContext>();
        
        context.Database.Migrate(); 
        Console.WriteLine("--> Veritabanı migrationları başarıyla uygulandı.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("--> Migration sırasında bir hata oluştu: " + ex.Message);
    }
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();