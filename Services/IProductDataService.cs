using SunriseCafe.Backend.Models;

namespace SunriseCafe.Backend.Services;

public interface IProductDataService
{
    Task<ProductModel?> FindProductByNameAsync(string productName);
    Task<List<ProductModel>> GetAllProducts(ProductCategory category);
    Task InsertProductAsync(ProductModel product);
}
