using GptBarv2.ViewModels;
using Microsoft.Maui.Controls;

namespace GptBarv2.Views
{
    [QueryProperty(nameof(BrandName), "brandName")]
    public partial class BrandDetailPage : ContentPage
    {
        public string BrandName { get; set; }
        public BrandDetailPage()
        {
            InitializeComponent();
            BindingContext = new BrandDetailViewModel();
        }
    }
}
