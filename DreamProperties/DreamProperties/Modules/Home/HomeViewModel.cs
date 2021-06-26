using DreamProperties.Common.Base;
using System;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DreamProperties.Modules.Home
{
    public class HomeViewModel: BaseViewModel
    {
        public AsyncCommand<string> SearchCommand { get => new AsyncCommand<string>(PerformSearch); }

        public AsyncCommand AddPropertyCommand { get => new AsyncCommand(AddProperty); }

        private Task AddProperty()
        {
            return Task.CompletedTask;
        }

        private Task PerformSearch(string propertyType)
        {
            return Task.CompletedTask;
        }
    }
}
