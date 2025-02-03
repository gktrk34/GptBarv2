using System.Collections.Generic;

namespace GptBarv2.Models
{
    public class BrandModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string ImageSource { get; set; } = string.Empty;
        public List<ProductModel> Products { get; set; } = new List<ProductModel>();
    }
}
