using Autofac;
using DreamProperties.Modules.Home;
using DreamProperties.Modules.Login;
using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Poppins-Regular.ttf", Alias = "AppRegular")]
[assembly: ExportFont("Poppins-Medium.ttf", Alias = "AppMedium")]
namespace DreamProperties
{
    public partial class App : Application
    {
        public static IContainer Container;

        public App()
        {
            InitializeComponent();

            //class used for registration
            var builder = new ContainerBuilder();
            //scan and register all classes in the assembly
            var dataAccess = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(dataAccess)
                   .AsImplementedInterfaces()
                   .AsSelf();

            //get container
            Container = builder.Build();

            MainPage = new AppShell();
        }
    }
}
