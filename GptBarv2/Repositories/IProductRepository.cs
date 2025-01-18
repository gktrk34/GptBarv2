using System.Collections.Generic;
using System.Threading.Tasks;
using GptBarv2.Models;

namespace GptBarv2.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetAllAsync();
        Task<ProductModel> GetByNameAsync(string name);
        Task<List<ProductModel>> GetSimilarByCategoryAsync(string category, string excludeName);
        Task UpdateRatingAsync(string name, int rating);
    }
}
