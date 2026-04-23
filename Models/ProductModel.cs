using System;

namespace SunriseCafe.Backend.Models;

public class ProductModel
{
    public Guid Id {get;set;}
    public string ImageUrl {get;set;}
    public string MobileImageUrl {get;set;}
    public string Name {get;set;}
    public decimal Price {get;set;}
    public string Description {get;set;}
    public ProductCategory Category {get;set;}
}
