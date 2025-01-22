using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GptBarv2.Data;
using GptBarv2.Models;

namespace GptBarv2.Repositories
{
    public class EFProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;

        public EFProductRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<ProductModel>> GetAllAsync()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<ProductModel> GetByNameAsync(string name)
        {
            return await _db.Products
                .FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<List<ProductModel>> GetSimilarByCategoryAsync(string category, string excludeName)
        {
            return await _db.Products
                .Where(p => p.Category == category && p.Name != excludeName)
                .ToListAsync();
        }

        // Tek metot: UpdateRatingAsync(productName, newRating)
        public async Task UpdateRatingAsync(string productName, int newRating)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Name == productName);
            if (product != null)
            {
                product.Rating = newRating;
                _db.Products.Update(product);
                await _db.SaveChangesAsync();
            }
        }
    }
}
