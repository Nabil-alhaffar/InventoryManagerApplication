using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using InventoryManager.Models;
using InventoryManager.Services;
using InventoryManager.Views;

namespace InventoryManager.ViewModels
{
    public class AddUserViewModel: BaseViewModel
    {
        //private AzureUserDataStore _azureDataStore = new AzureUserDataStore();

        public User user { get; set;}
       

        public ICommand AddUserCommand
        {
            get
            {
                return new Command(async () =>
                {
                    bool isSuccess = await UserDataStore.AddUserAsync(user);

                    Application.Current.MainPage = new NavigationPage(new MainPage());

                });
            }

        }
    }
}
