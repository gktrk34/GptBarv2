using GptBarv2.Data;
using GptBarv2.Models;
using Microsoft.EntityFrameworkCore;

namespace GptBarv2.Repositories
{
    public class EFBrandRepository : IBrandRepository
    {
        private readonly AppDbContext _db;

        public EFBrandRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<BrandModel> GetByNameAsync(string brandName)
        {
            // Marka + ilişkili ürünleri yükle
            return await _db.Brands
                .Include(b => b.Products)
                .FirstOrDefaultAsync(b => b.Name == brandName);
        }
    }
}
