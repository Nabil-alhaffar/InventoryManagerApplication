using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using InventoryManager.Models;
using InventoryManager.Views;
using InventoryManager.ViewModels;

namespace InventoryManager.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Product;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {

            //  await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
            if (App.currentUser.UserType == "Admin" || App.currentUser.UserType == "DistrictManager")
                await Navigation.PushModalAsync(new NavigationPage(new AddNewProductPage()));
            else
            {

                string message = "User is not authorized to access these resources.";

                DisplayAlert("Error authenticating user", message, "OK");
                return;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Products.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}