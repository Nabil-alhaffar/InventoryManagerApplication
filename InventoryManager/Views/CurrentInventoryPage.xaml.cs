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
    public partial class CurrentInventoryPage : ContentPage
    {
        StocksViewModel viewModel;

        public CurrentInventoryPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new StocksViewModel();

        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            
            var stock = args.SelectedItem as Stock;
            if (stock == null)
                return;
            if (stock.SerialNumbers.Count() != 0)
            {
                await Navigation.PushAsync(new StocksDetailPage(new StocksDetailViewModel(stock)));
            }
            // Manually deselect item.
            StocksListView.SelectedItem = null;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Stocks.Count == 0)
                viewModel.LoadStocksCommand.Execute(null);
        }
    }
}