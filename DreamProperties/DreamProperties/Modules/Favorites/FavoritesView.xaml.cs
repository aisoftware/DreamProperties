using Autofac;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DreamProperties.Modules.Favorites
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoritesView : ContentPage
    {
        public FavoritesView()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<FavoritesViewModel>();
        }
    }
}