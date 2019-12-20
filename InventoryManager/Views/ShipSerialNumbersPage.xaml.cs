using System;
using System.Collections.Generic;
using InventoryManager.Models;
using Xamarin.Forms;
using InventoryManager.ViewModels;
using System.Threading.Tasks;
using System.Linq;
namespace InventoryManager.Views
{
    public partial class ShipSerialNumbersPage : ContentPage
    {
        ShipSerialNumbersViewModel viewModel;
        public IEnumerable<Stock> Stocks;


        public ShipSerialNumbersPage(ShipSerialNumbersViewModel viewModel, IEnumerable<Stock>stocks, int receivingStoreId)
        {
            InitializeComponent();
            Stocks = stocks;
            viewModel.ReceivingStoreId = receivingStoreId;
            BindingContext = this.viewModel ;
        }
        public async void OnPostSerialNumbersClicked(object sender, EventArgs args)
        {
            var actionSheet = await DisplayActionSheet("Are you sure you would like post the stocks? These stocks will be shipped.", "Cancel", "Confirm Post");

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

                        MessagingCenter.Send(this, "ShipMixedStocks", Stocks);
                        await Navigation.PopAsync();
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
