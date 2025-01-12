using Microsoft.Maui.Controls;

namespace GptBarv2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // İleride rota (Route) tanımlamak istersen:
            // Routing.RegisterRoute("BrandDetailPage", typeof(BrandDetailPage));
            Routing.RegisterRoute("CategoryDetailPage", typeof(GptBarv2.Views.CategoryDetailPage));
            Routing.RegisterRoute("BrandDetailPage", typeof(GptBarv2.Views.BrandDetailPage));
            Routing.RegisterRoute("ProductDetailPage", typeof(GptBarv2.Views.ProductDetailPage));

        }
    }
}
