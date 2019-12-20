using InventoryManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InventoryManager.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem { Id = MenuItemType.WelcomePage,Title= "Welcome Page"},
                new HomeMenuItem {Id = MenuItemType.ProductManager, Title="Product Manager" },
                new HomeMenuItem {Id = MenuItemType.Reconcile, Title = "Reconcile" },
                new HomeMenuItem {Id = MenuItemType.CurrentStoreStocks, Title = "Current Stocks" },
                new HomeMenuItem {Id = MenuItemType.InventoryTransfers, Title = "Inventory Transfers" },
                new HomeMenuItem {Id = MenuItemType.PastOrders, Title = "Past Orders" },
                new HomeMenuItem {Id = MenuItemType.About, Title="About" }




            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
        public async void OnLogoutClicked(object sender, EventArgs args )
        {
            App.currentUser = null;
            App.currentStore = null;
            App.Current.MainPage = new LoginPage();
      

        }
    }
}