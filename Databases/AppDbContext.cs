using Microsoft.EntityFrameworkCore;
using SunriseCafe.Backend.Models;

namespace SunriseCafe.Backend.Databases;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<ProductModel> Products { get; set; }
}
