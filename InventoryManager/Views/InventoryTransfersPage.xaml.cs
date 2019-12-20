using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace InventoryManager.Views
{
    public partial class InventoryTransfersPage : ContentPage
    {
        public InventoryTransfersPage()
        {
            InitializeComponent();
        }
        public void OnShipStocksClicked(object sender, EventArgs args)
        {
            if (App.currentUser.UserType == "Admin" || App.currentUser.UserType == "DistrictManager")
                Navigation.PushAsync(new ShipStocksPage());

            else
            {

                string message = "User is not authorized to access these resources.";

                DisplayAlert("Error authenticating user", message, "OK");
                return;
            }
        }
        public void OnReceivePurchuseOrder(object sender, EventArgs args)
        {
            if (App.currentUser.UserType == "Admin" || App.currentUser.UserType == "DistrictManager")

                Navigation.PushAsync(new PurchaseOrderPage());
            else
            {

                string message = "User is not authorized to access these resources.";

                DisplayAlert("Error authenticating user", message, "OK");
                return;
            }
        }
    }
}
