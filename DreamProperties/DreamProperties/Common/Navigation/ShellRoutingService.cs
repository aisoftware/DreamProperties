using DreamProperties.Common.Base;
using DreamProperties.Modules.Login;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DreamProperties.Common.Navigation
{
    public interface INavigationService
    {
        void GoToMainFlow();
        void GoToLoginFlow();
        Task PushAsync<TViewModel>() where TViewModel : BaseViewModel;
        Task PushAsync<TViewModel,TModel>(TModel model) where TViewModel : BaseViewModel;
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

        public async Task PushAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            ShellNavigationState state = Shell.Current.CurrentState;
            await Shell.Current.GoToAsync($"{state.Location}/{typeof(TViewModel).Name}");
        }

        public async Task PushAsync<TViewModel,TModel>(TModel model) where TViewModel : BaseViewModel
        {
            var parameter = string.Empty;

            if (model != null)
            {
                parameter = JsonConvert.SerializeObject(model);
                parameter = Uri.EscapeDataString(parameter);
            }

            ShellNavigationState state = Shell.Current.CurrentState;
            await Shell.Current.GoToAsync($"{state.Location}/{typeof(TViewModel).Name}?parameter={parameter}");
        }
    }
}

