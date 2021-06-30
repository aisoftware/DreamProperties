using DreamProperties.Common.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;

namespace DreamProperties.Modules.Settings
{
    public class SettingsViewModel: BaseViewModel
    {
        public AsyncCommand LogoutCommand { get => new AsyncCommand(PerformLogout); }

        private Task PerformLogout()
        {
            return Task.CompletedTask;
        }
    }
}
