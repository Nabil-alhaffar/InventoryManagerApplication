using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using InventoryManager.Models;
using InventoryManager.ViewModels;

using Xamarin.Forms;

namespace InventoryManager.Views
{
    public partial class StocksDetailPage : ContentPage
    {
       StocksDetailViewModel viewModel;

        public StocksDetailPage(StocksDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;

        }
        public StocksDetailPage()
        {
            InitializeComponent();

            var stock = new Stock
            {
                //Text = "Item 1",
            };

            viewModel = new StocksDetailViewModel(stock);
            BindingContext = viewModel;
        }
    }
}
