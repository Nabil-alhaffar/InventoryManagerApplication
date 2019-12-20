using System;
using System.Collections.Generic;
using InventoryManager.ViewModels;
using Xamarin.Forms;
using InventoryManager.Models;
namespace InventoryManager.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<LoginViewModel>(this, "AuthenticationFailed", (async (obj) =>
            {
                string message = "User was not authenticated. Please re-enter your cridentials, or contact your manager to reset your password. ";
             
                await DisplayAlert("Error authenticating user", message, "OK");
                return;
            }));
        }

    }
}
