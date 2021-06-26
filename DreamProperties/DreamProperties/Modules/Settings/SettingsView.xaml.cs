using Autofac;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DreamProperties.Modules.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsView : ContentPage
    {
        public SettingsView()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<SettingsViewModel>();
        }
    }
}