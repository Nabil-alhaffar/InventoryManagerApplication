using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using InventoryManager.Models;
using InventoryManager.ViewModels;

namespace InventoryManager.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var product = new Product
            {
                //Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(product);
            BindingContext = viewModel;
        }
    }
}