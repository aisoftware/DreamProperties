using DreamProperties.Modules.Login;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Poppins-Regular.ttf", Alias = "AppRegular")]
[assembly: ExportFont("Poppins-Medium.ttf", Alias = "AppMedium")]
namespace DreamProperties
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginView();
        }
    }
}
