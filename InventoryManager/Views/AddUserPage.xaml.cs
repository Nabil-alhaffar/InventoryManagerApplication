using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InventoryManager.Models;

namespace InventoryManager.Views
{
    public partial class AddUserPage : ContentPage
    {
        public User User { get; set; }
        public AddUserPage()
        {
            InitializeComponent();
            User = new User();
            BindingContext = this;

        }
        async void OnCancelClicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();

        }
        async void OnSaveClicked(object sender, EventArgs args)
        {
            MessagingCenter.Send(this, "AddUser", User);
            await Navigation.PopModalAsync();

        }
       
    }
}
