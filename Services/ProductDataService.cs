using Microsoft.EntityFrameworkCore;
using SunriseCafe.Backend.Databases;
using SunriseCafe.Backend.Models;

namespace SunriseCafe.Backend.Services;

public class ProductDataService : IProductDataService
{
    private readonly AppDbContext _dbContext;
    public ProductDataService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductModel?> FindProductByNameAsync(string productName)
    {
        var product = await _dbContext.Products.SingleOrDefaultAsync(x => x.Name.Equals(productName));

        return product;
    }

    public async Task<List<ProductModel>> GetAllProducts(ProductCategory category)
    {
        var products = (category == ProductCategory.All) 
            ? await _dbContext.Products.ToListAsync() 
            : await _dbContext.Products.Where(x => x.Category.Equals(category)).ToListAsync();

        return products;
    }

    public async Task InsertProductAsync(ProductModel product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }
}
