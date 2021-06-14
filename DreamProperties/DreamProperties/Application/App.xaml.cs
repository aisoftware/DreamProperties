using DreamProperties.Modules.Login;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
