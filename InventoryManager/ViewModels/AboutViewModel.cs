using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace InventoryManager.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://csserver.evansville.edu/~na60/actualProject/proposal.pdf")));
        }

        public ICommand OpenWebCommand { get; }
    }
}