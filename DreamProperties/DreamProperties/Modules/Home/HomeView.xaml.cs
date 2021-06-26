using Autofac;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DreamProperties.Modules.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<HomeViewModel>();
        }

        protected override async void OnAppearing()
        {
            await (BindingContext as HomeViewModel).InitializeAsync(null);
            base.OnAppearing();
        }
    }
}