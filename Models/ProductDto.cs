namespace SunriseCafe.Backend.Models;

public class ProductDto
{
    public string ImageUrl { get; private set; }
    public string MobileImageUrl { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public string Description { get; private set; }
    public string Category { get; private set; }

    public static ProductDto MapFromModel(ProductModel model)
    {
        return new ProductDto()
        {
            ImageUrl = model.ImageUrl,
            MobileImageUrl = model.MobileImageUrl,
            Name = model.Name,
            Price = model.Price,
            Description = model.Description,
            Category = model.Category.ToString()
        };
    }
}
