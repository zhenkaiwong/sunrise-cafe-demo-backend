using SunriseCafe.Backend.Models;
using SunriseCafe.Backend.Services;

namespace SunriseCafe.Backend.Endpoints;

public static class ProductEndpoints
{
    private static async Task<IResult> FindProductByName(string productName, IProductDataService productDataService)
    {
        var product = await productDataService.FindProductByNameAsync(productName);

        if (product is null)
        {
            return TypedResults.NotFound($"Cannot find product with name '{productName}'");
        }

        return TypedResults.Ok(ProductDto.MapFromModel(product));
    }
    private static async Task<IResult> GetAllProducts(string category, IProductDataService productDataService)
    {
        ProductCategory? resolvedCategory = category.ToLower() switch
        {
            "coffee" => ProductCategory.Coffee,
            "noncoffee" => ProductCategory.NonCoffee,
            "pastry" => ProductCategory.Pastry,
            "all" => ProductCategory.All,
            _ => null
        };

        if (resolvedCategory is null)
        {
            return TypedResults.Problem($"Category '{category}' is not valid");
        }

        var products = await productDataService.GetAllProducts((ProductCategory) resolvedCategory);

        return TypedResults.Ok(products.Select(x => ProductDto.MapFromModel(x)).ToList());
    }

    public static void RegisterProductEndpoints(this WebApplication webApp)
    {
        var group = webApp.MapGroup("/api/products");

        group.MapGet("/name/{productName}", FindProductByName);
        group.MapGet("/category/{category}", GetAllProducts);
    }
}
