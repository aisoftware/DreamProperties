using DreamProperties.Modules.AddProperty;
using DreamProperties.Modules.PropertyListing;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DreamProperties
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(typeof(AddPropertyViewModel).Name, typeof(AddPropertyView));
            Routing.RegisterRoute(typeof(PropertyListingViewModel).Name, typeof(PropertyListingView));
        }
    }
}