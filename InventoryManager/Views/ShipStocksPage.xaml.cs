using System;
using System.Collections.Generic;
using InventoryManager.Models;
using InventoryManager.ViewModels;
using Xamarin.Forms;
using System.Threading;
using System.Linq;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace InventoryManager.Views
{
    public partial class ShipStocksPage : ContentPage
    {
        ShipStocksViewModel viewModel;

        public ShipStocksPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ShipStocksViewModel();
            viewModel.Stocks.Add(new Stock(App.currentStore.StoreId));

        }
        public void OnAddStocksClicked(object sender, EventArgs args)
        {

            viewModel.Stocks.Add(new Stock(App.currentStore.StoreId));

        }
        public async void OnShipStocksClicked(object sender, EventArgs args)
        {
            var actionSheet = await DisplayActionSheet("Are you sure you would like post the stocks? These stocks will be shipped.", "Cancel", "Confirm Post");

            switch (actionSheet)
            {
                case "Cancel":

                    // Do Something when 'Cancel' Button is pressed

                    break;
                case "Confirm Post":
                    {
                        ObservableCollection<SerialNumber> serialNumbers = new ObservableCollection<SerialNumber>();
                        IEnumerable<Product> products = await viewModel.DataStore.GetProductsAsync();
                        IEnumerable<Stock> stocks = viewModel.Stocks;
                        foreach (Stock stock in stocks)
                        {
                            Product product = products.FirstOrDefault(x => x.Id == stock.ProductId);
                            if (product == null)
                            {
                                string template = "Product {0} was not found. Please remove from document. ";
                                string productId = stock.ProductId.ToString();
                                string message = string.Format(template, productId);
                                await DisplayAlert("Error adding stocks", message, "OK");
                                return;
                            }
                            if (product.ProductType.ToLower() == "phone")
                            {
                                for (int i = 0; i < stock.Quantity; i++)
                                {
                                    serialNumbers.Add(new SerialNumber { ProductId = stock.ProductId, StoreId = stock.StoreId });
                                }
                            }

                        }
                        if (serialNumbers.Count != 0)
                        {

                            await Navigation.PushAsync(new ShipSerialNumbersPage(new ShipSerialNumbersViewModel(serialNumbers), viewModel.Stocks, viewModel.ReceivingStoreId));
                        }
                        else
                        {
                            MessagingCenter.Send(this, "ShipNonSerialStocks", stocks);
                            await Navigation.PopAsync();

                        }
                        break;
                    }

            }
        }

        public async void OnCancelClicked(object sender, EventArgs args)
        {
            var actionSheet = await DisplayActionSheet("Are you sure you would like to Cancel? ", "Cancel", "Confirm Cancellation");

            switch (actionSheet)
            {
                case "Cancel":

                    // Do Something when 'Cancel' Button is pressed

                    break;
                case "Confirm Cancellation":
                    await Navigation.PopAsync();


                    break;

            }


        }
    }

   
}

