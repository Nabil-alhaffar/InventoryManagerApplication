using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using  System.Text;
using InventoryManager.Models;
using Xamarin.Forms;
using System.Windows.Input;
using System.Diagnostics;
using System.Collections.ObjectModel;
using InventoryManager.Views;
 
using InventoryManager.Services;

namespace InventoryManager.ViewModels
{

    public class LoginViewModel: BaseViewModel
    {
        public ObservableCollection<User> Users { get; set; }
        public Command LoadUsersCommand { get; set; }

        public AuthenticationModel AuthenticationModel { get; set; }

        public LoginViewModel()
        {
            AuthenticationModel = new AuthenticationModel();
            

        }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var user = await UserDataStore.AuthenticateUserAsync( AuthenticationModel);

                    if (user.Token != null)
                    {
                        Store store = await StoreDataStore.GetStoreByStoreNameAsync(AuthenticationModel.StoreId);
                        App.currentUser = user;
                        App.currentStore = store;

                        Application.Current.MainPage = new MainPage();

                    }
                    else
                    {
                        MessagingCenter.Send(this, "AuthenticationFailed");

                        //string template = "Product {0} was not found. Please remove from document. ";
                        //string productId = stock.ProductId.ToString();
                        //string message = string.Format(template, productId);
                        //await DisplayAlert("Error adding stocks", message, "OK");
                        //return;

                    }
                    //     Application.Current.MainPage = new NavigationPage(new MenuPage()); 
                    //Application.Current.MainPage = new MainPage();

                });
            }

        }

    }
}
