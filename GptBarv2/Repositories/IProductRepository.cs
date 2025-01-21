using GptBarv2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GptBarv2.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetAllAsync();
        Task<ProductModel?> GetByNameAsync(string name);
        Task<List<ProductModel>> GetSimilarByCategoryAsync(string category, string productName);
        Task UpdateRatingAsync(string productName, int rating);

    }
}