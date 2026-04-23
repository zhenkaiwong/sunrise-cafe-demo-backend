using Microsoft.EntityFrameworkCore;
using SunriseCafe.Backend.Databases;
using SunriseCafe.Backend.Endpoints;
using SunriseCafe.Backend.Models;
using SunriseCafe.Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("app_db")
    .UseAsyncSeeding(async (context, _, cancellationToken) =>
    {
        // context.Set<ProductModel>().Add(new ProductModel
        // {
        //     Id = Guid.NewGuid(),
        //     ImageUrl = "",
        //     MobileImageUrl = "",
        //     Name = "",
        //     Price = 0,
        //     Description = "",
        //     Category = ProductCategory.All
        // });
        context.Set<ProductModel>().Add(new ProductModel
        {
            Id = Guid.NewGuid(),
            ImageUrl = "/products/latte-product.jpg",
            MobileImageUrl = "/products/latte-thumbnail.jpg",
            Name = "Caffè Latte",
            Price = 12.5m,
            Description = "Smooth and creamy espresso with steamed milk, offering a comforting and balanced flavor.",
            Category = ProductCategory.Coffee
        });
        context.Set<ProductModel>().Add(new ProductModel
        {
            Id = Guid.NewGuid(),
            ImageUrl = "/products/americano-product.jpg",
            MobileImageUrl = "/products/americano-thumbnail.jpg",
            Name = "Americano",
            Price = 10.0m,
            Description = "A rich, bold espresso diluted with hot water, creating a smooth yet strong coffee experience.",
            Category = ProductCategory.Coffee
        });
        context.Set<ProductModel>().Add(new ProductModel
        {
            Id = Guid.NewGuid(),
            ImageUrl = "/products/hot-chocolate-product.jpg",
            MobileImageUrl = "/products/hot-chocolate-thumbnail.jpg",
            Name = "Hot Chocolate",
            Price = 11.0m,
            Description = "Luxuriously rich chocolate blended with steamed milk, topped with a light foam.",
            Category = ProductCategory.NonCoffee
        });
        context.Set<ProductModel>().Add(new ProductModel
        {
            Id = Guid.NewGuid(),
            ImageUrl = "/products/croissant-product.jpg",
            MobileImageUrl = "/products/croissant-thumbnail.jpg",
            Name = "Butter Croissant",
            Price = 8.0m,
            Description = "Flaky, golden layers of buttery pastry, baked fresh every morning for the perfect bite.",
            Category = ProductCategory.Pastry
        });

        await context.SaveChangesAsync();
    }));

builder.Services.AddScoped<IProductDataService, ProductDataService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.RegisterProductEndpoints();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // EnsureCreatedAsync ensures the database exists; if not, it creates it
    await context.Database.EnsureCreatedAsync();
}

app.Run();
