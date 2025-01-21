using GptBarv2.Data;
using GptBarv2.Models;
using GptBarv2.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class EFProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public EFProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductModel>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<ProductModel?> GetByNameAsync(string name)
    {
        return await _context.Products
            .Include(p => p.Brand)
            .FirstOrDefaultAsync(p => p.Name == name);
    }
    public async Task<List<ProductModel>> GetSimilarByCategoryAsync(string category, string productName)
    {
        return await _context.Products
            .Where(p => p.Category == category && p.Name != productName)
            .ToListAsync();
    }

    public Task UpdateRatingAsync(string productName, int rating)
    {
        throw new NotImplementedException();
    }
}