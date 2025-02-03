namespace GptBarv2.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;         // Örneğin: "Gordon's London Dry"
        public string Description { get; set; } = string.Empty;
        public string ImageSource { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Rating { get; set; }
        public string Category { get; set; } = string.Empty;
        public string TastingNotes { get; set; } = string.Empty;
        public string AdditionalInfo { get; set; } = string.Empty;
        public int BrandId { get; set; }
        public BrandModel Brand { get; set; } = new BrandModel();
    }
}
