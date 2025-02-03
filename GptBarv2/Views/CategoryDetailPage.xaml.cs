using GptBarv2.ViewModels;
using Microsoft.Maui.Controls;

namespace GptBarv2.Views
{
    [QueryProperty(nameof(CategoryName), "category")]
    public partial class CategoryDetailPage : ContentPage
    {
        public string CategoryName { get; set; }
        public CategoryDetailPage()
        {
            InitializeComponent();
            BindingContext = new CategoryDetailViewModel();
        }
    }
}
