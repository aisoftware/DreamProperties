using DreamProperties.Common.Base;
using DreamProperties.Modules.Login;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DreamProperties.Common.Navigation
{
    public interface INavigationService
    {
        Task PushAsync<TViewModel>(string parameters = null) where TViewModel : BaseViewModel;
        Task PopAsync();
        Task InsertAsRoot<TViewModel>(string parameters = null) where TViewModel : BaseViewModel;
        Task GoBackAsync();
        void GoToMainFlow();
        void GoToLoginFlow();
    }

    public class ShellRoutingService : INavigationService
    {
        public void GoToMainFlow()
        {
            App.Current.MainPage = new AppShell();
        }

        public void GoToLoginFlow()
        {
            App.Current.MainPage = new LoginView();
        }

        public Task PopAsync()
        {
            return Shell.Current.Navigation.PopAsync();
        }

        public Task GoBackAsync()
        {
            return Shell.Current.GoToAsync("..");
        }

        public Task InsertAsRoot<TViewModel>(string parameters = null) where TViewModel : BaseViewModel
        {
            return GoToAsync<TViewModel>("//", parameters);
        }

        public Task PushAsync<TViewModel>(string parameters = null) where TViewModel : BaseViewModel
        {
            return GoToAsync<TViewModel>("", parameters);
        }

        /*
                public async void NavigateTo<T>(string route, T model, string title = null)
        {
            var parameter = string.Empty;

            if(model != null)
            {
                parameter = JsonConvert.SerializeObject(model);
                parameter = Uri.EscapeDataString(parameter);
            }

            ShellNavigationState state = Shell.Current.CurrentState;
            await Shell.Current.GoToAsync($"{state.Location}/{route}?parameter={parameter}&title={title}");
            Shell.Current.FlyoutIsPresented = false;
        }
        */

        private Task GoToAsync<TViewModel>(string routePrefix, string parameters) where TViewModel : BaseViewModel
        {
            var route = routePrefix + typeof(TViewModel).Name;
            if (!string.IsNullOrWhiteSpace(parameters))
            {
                route += $"?{parameters}";
            }
            return Shell.Current.GoToAsync(route);
        }
    }
}

