using DreamProperties.Modules.AddProperty;

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

            Routing.RegisterRoute("AddPropertyViewModel", typeof(AddPropertyView));
        }
    }
}