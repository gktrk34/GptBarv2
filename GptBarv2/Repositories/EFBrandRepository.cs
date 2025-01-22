using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GptBarv2.Data;
using GptBarv2.Models;

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
            // marka + ürünler
            return await _db.Brands
                .Include(b => b.Products)
                .FirstOrDefaultAsync(b => b.Name == brandName);
        }

        // Yeni eklenen metot
        public async Task<List<BrandModel>> GetAllByCategoryAsync(string category)
        {
            // Bu sorgu, EF Core ile "category" alanı eşleşen markaları getirir.
            return await _db.Brands
                .Where(b => b.Category == category)
                .Include(b => b.Products)  // Markaların ürünlerini de yüklemek istersek
                .ToListAsync();
        }
    }
}
