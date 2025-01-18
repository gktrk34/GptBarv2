using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GptBarv2.Models
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageSource { get; set; }
        public double Price { get; set; }
        public int Rating { get; set; }
        public string Category { get; set; }
        public string TastingNotes { get; set; }
        public string AdditionalInfo { get; set; }

        // Marka ilişkisi
        public int BrandId { get; set; }
        public BrandModel Brand { get; set; } // Navigation property
    }
}
