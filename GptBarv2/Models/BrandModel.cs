using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace GptBarv2.Models
{
    public class BrandModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;     // "Gordon's", "Absolut" vb.
        public string Category { get; set; } = string.Empty;   // "Gin", "Vodka", vs.
        public string ImageSource { get; set; } = string.Empty; // Markanın logosu yolunu tutar

        // Bir markanın birden çok ürünü olabilir
        public List<ProductModel> Products { get; set; } = new List<ProductModel>();
    }
}