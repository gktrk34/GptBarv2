using GptBarv2.Views;

namespace GptBarv2;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("CategoryDetailPage", typeof(CategoryDetailPage));
        Routing.RegisterRoute("BrandDetailPage", typeof(BrandDetailPage));
        Routing.RegisterRoute("ProductDetailPage", typeof(ProductDetailPage));
    }
}