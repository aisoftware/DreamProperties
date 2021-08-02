using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DreamProperties.Modules.PropertyListing
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PropertyListingView : ContentPage
    {
        public PropertyListingView()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<PropertyListingViewModel>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as PropertyListingViewModel).InitializeAsync();
        }
    }
}