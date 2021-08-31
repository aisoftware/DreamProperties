using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DreamProperties.Common.Dialog
{
    public interface IDialogMessage
    {
        Task DisplayOkAlert(string title, string message);
    }

    public class DialogMessage : IDialogMessage
    {
        public async Task DisplayOkAlert(string title, string message)
        {
            await Shell.Current.DisplayAlert(title, message, "Ok");
        }
    }
}
