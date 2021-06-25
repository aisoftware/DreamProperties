using DreamProperties.Common.Base;
using DreamProperties.Common.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DreamProperties
{
    public class AppShellViewModel: BaseViewModel
    {
        private INavigationService _navigationService;

        public AppShellViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
