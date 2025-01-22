using GptBarv2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GptBarv2.Repositories
{
    public interface IBrandRepository
    {
        // Tek bir markayı ismine göre getir
        Task<BrandModel> GetByNameAsync(string brandName);

        // Kategorisine göre tüm markaları getir
        Task<List<BrandModel>> GetAllByCategoryAsync(string category);
    }
}
