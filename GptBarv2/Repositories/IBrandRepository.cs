using GptBarv2.Models;

namespace GptBarv2.Repositories
{
    public interface IBrandRepository
    {
        Task<BrandModel> GetByNameAsync(string brandName);
        // Gerekirse: Task<List<BrandModel>> GetAllByCategoryAsync(string category) vs.
    }
}
