using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace InventoryManager.Views
{
    public partial class InventoryReconciliationPage : ContentPage
    {
        public InventoryReconciliationPage()
        {
            InitializeComponent();
        }

        public void OnAddClicked(object sender, EventArgs args)
        {
            if (App.currentUser.UserType == "Admin" || App.currentUser.UserType == "DistrictManager")
            {

                Navigation.PushAsync(new AddStocksView());
            }
            else {

                string message = "User is not authorized to access these resources.";

                 DisplayAlert("Error authenticating user", message, "OK");
                return;
            }
        }
        public void OnSubtractClicked(object sender, EventArgs args) {

            if (App.currentUser.UserType == "Admin" || App.currentUser.UserType == "DistrictManager")
            {
                Navigation.PushAsync(new SubtractStocksPage());
            }

            else
            {

                string message = "User is not authorized to access these resources.";

                DisplayAlert("Error authenticating user", message, "OK");
                return;
            }

        }
        public void OnInventoryCountAndReconciliationClicked(object sender, EventArgs args) {
            if (App.currentUser.UserType == "Admin" || App.currentUser.UserType == "DistrictManager")
            {
                Navigation.PushAsync(new CountStockAndReconciliationPage());
            }
            else
            {

                string message = "User is not authorized to access these resources.";

                DisplayAlert("Error authenticating user", message, "OK");
                return;
            }
        }
    }
}
