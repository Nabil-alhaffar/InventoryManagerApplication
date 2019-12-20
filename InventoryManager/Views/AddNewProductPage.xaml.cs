using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InventoryManager.Models;

namespace InventoryManager.Views
{
    public partial class AddNewProductPage : ContentPage
    {

        public Product Product { get; set; }

        public AddNewProductPage()
        {
            InitializeComponent();
            Product = new Product
            {

                //Text = "Item name",
                //Description = "This is an item description."
            };

            BindingContext = this;
        }

        async void OnCancelClicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();

        }
        async void OnSaveClicked(object sender, EventArgs args)
        {
            MessagingCenter.Send(this, "AddItem", Product);
            await Navigation.PopModalAsync();

        }
        void SelectedIndexChanged(object sender, EventArgs args)
        {
            Picker picker = sender as Picker;
            var selectedIndex = picker.SelectedIndex;
            if (selectedIndex == 0)
            {
                Product.ProductType = "Phone";
                phoneItemStackView.IsVisible = true;
                accessoryItemStackView.IsVisible = false;
                Product.AccessoryItemType =  "N/A";

            }
            if (selectedIndex == 1)
            {
                Product.ProductType = "Accessory";

                phoneItemStackView.IsVisible = false;
                accessoryItemStackView.IsVisible = true;

            }

        }
    }
}
