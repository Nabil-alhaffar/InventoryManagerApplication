﻿
using System;
using System.Collections.Generic;
using InventoryManager.Models;
using Xamarin.Forms;
using InventoryManager.ViewModels;
using System.Threading.Tasks;
using System.Linq;

namespace InventoryManager.Views
{
    public partial class CountStockAndReconciliationSerialNumbersPage : ContentPage
    {
        public Command GetSerialFieldsCommand { get; set; }
        CountStockAndReconciliationSerialNumbersViewModel viewModel;
        public IEnumerable<Stock> Stocks;

        public CountStockAndReconciliationSerialNumbersPage()
        {
            InitializeComponent();
        }
        public CountStockAndReconciliationSerialNumbersPage(CountStockAndReconciliationSerialNumbersViewModel viewModel, IEnumerable<Stock> stocks) {

            InitializeComponent();
            Stocks = stocks;
            BindingContext = this.viewModel = viewModel;

        }
        public async Task ExecuteGetSerialFields()
        {

            try
            {
                IEnumerable<Product> products = await viewModel.DataStore.GetProductsAsync();


                foreach (Stock stock in Stocks)
                {
                    Product product = products.FirstOrDefault(x => x.Id == stock.ProductId);

                    if (product.ProductType.ToLower() == "phone")
                    {
                        for (int i = 0; i < stock.Quantity; i++)
                        {
                            viewModel.SerialNumbers.Add(new SerialNumber(stock.StoreId, stock.ProductId));
                        }
                    }
                }
            }
            catch { }

        }
        public async void OnPostSerialNumbersClicked(object sender, EventArgs args)
        {
            var actionSheet = await DisplayActionSheet("Are you sure you would like post the stocks? These stocks will replace current store inventory.", "Cancel", "Confirm Post");

            switch (actionSheet)
            {
                case "Cancel":

                    // Do Something when 'Cancel' Button is pressed

                    break;
                case "Confirm Post":
                    {
                        IEnumerable<SerialNumber> serialNumbers = viewModel.SerialNumbers;
                        IEnumerable<Stock> stocks = viewModel.Stocks;

                        foreach (Stock stock in Stocks)
                            stock.SerialNumbers = serialNumbers.Where(x => x.ProductId == stock.ProductId).ToList();

                        //foreach (Stock stock in Stocks) {
                        //    stock.SerialNumbers = stocks.
                        //}

                        MessagingCenter.Send(this, "PostMixedStocksCount", Stocks);
                        await Navigation.PopAsync();
                        break;
                        //MessagingCenter.Send(this, "AddSerialNumbers", serialNumbers);
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

