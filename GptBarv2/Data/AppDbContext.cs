using Microsoft.EntityFrameworkCore;
using GptBarv2.Models;

namespace GptBarv2.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<BrandModel> Brands { get; set; }
        public DbSet<ProductModel> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
