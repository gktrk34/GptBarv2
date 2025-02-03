using GptBarv2.ViewModels;
using Microsoft.Maui.Controls;

namespace GptBarv2.Views
{
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
            BindingContext = new WelcomeViewModel();
        }
    }
}
