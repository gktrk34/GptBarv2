using GptBarv2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GptBarv2.Repositories
{
    public interface IBrandRepository
    {
        Task<List<BrandModel>> GetAllByCategoryAsync(string categoryName);
        Task<BrandModel?> GetByNameAsync(string name);
    }
}