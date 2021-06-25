using Autofac;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DreamProperties.Modules.AddProperty
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPropertyView : ContentPage
    {
        public AddPropertyView()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<AddPropertyViewModel>();
        }
    }
}