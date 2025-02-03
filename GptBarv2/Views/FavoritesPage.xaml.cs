using GptBarv2.ViewModels;
using Microsoft.Maui.Controls;

namespace GptBarv2.Views
{
    public partial class FavoritesPage : ContentPage
    {
        public FavoritesPage()
        {
            InitializeComponent();
            BindingContext = new FavoritesViewModel();
        }
    }
}
