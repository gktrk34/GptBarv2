using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GptBarv2.Models
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageSource { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Rating { get; set; }
        public string Category { get; set; } = string.Empty;
        public string TastingNotes { get; set; } = string.Empty;
        public string AdditionalInfo { get; set; } = string.Empty;

        // Marka ilişkisi
        public int BrandId { get; set; }
        public BrandModel? Brand { get; set; } // Navigation property
    }
}