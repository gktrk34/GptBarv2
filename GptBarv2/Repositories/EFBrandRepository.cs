using GptBarv2.Data;
using GptBarv2.Models;
using GptBarv2.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class EFBrandRepository : IBrandRepository
{
    private readonly AppDbContext _context;

    public EFBrandRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<BrandModel>> GetAllByCategoryAsync(string categoryName)
    {
        return await _context.Brands
            .Where(b => b.Category == categoryName)
            .ToListAsync();
    }

    public async Task<BrandModel?> GetByNameAsync(string name)
    {
        return await _context.Brands
            .Include(b => b.Products)
            .FirstOrDefaultAsync(b => b.Name == name);
    }
}