using GptBarv2.ViewModels;
using Microsoft.Maui.Controls;

namespace GptBarv2.Views
{
    [QueryProperty(nameof(ProductName), "productName")]
    public partial class ProductDetailPage : ContentPage
    {
        public string ProductName { get; set; } = string.Empty;
        public ProductDetailPage()
        {
            InitializeComponent();
            BindingContext = new ProductDetailViewModel();
        }
    }
}
