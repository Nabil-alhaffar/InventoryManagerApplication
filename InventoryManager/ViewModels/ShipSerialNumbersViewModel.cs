using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using InventoryManager.Models;
using InventoryManager.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Input;

namespace InventoryManager.ViewModels
{
    public class ShipSerialNumbersViewModel :BaseViewModel 
    {
        public int ReceivingStoreId { get; set; }
        public IEnumerable<Stock> Stocks { get; set; }
        public Command GetSerialFieldsCommand { get; set; }
        public ObservableCollection<SerialNumber> SerialNumbers { get; set; }
        public ShipSerialNumbersViewModel()
        {

            Title = "Ship Serial Numbers";
            Stocks = new List<Stock>();
            SerialNumbers = new ObservableCollection<SerialNumber>();
            GetSerialFieldsCommand = new Command(async () => await ExecuteGetSerialFields());
        }
        public ShipSerialNumbersViewModel(ObservableCollection<SerialNumber> serialNumbers)
        {

            SerialNumbers = new ObservableCollection<SerialNumber>();

            Title = "Ship Serial Numbers";
            foreach (SerialNumber imei in serialNumbers)
                SerialNumbers.Add(imei);

            MessagingCenter.Subscribe<ShipSerialNumbersPage, IEnumerable<Stock>>(this, "ShipMixedStocks", (async (obj, stocks) =>
            {
                var shippedStocks = stocks as IEnumerable<Stock>;
                await StoreDataStore.SubtractStocksAsync(App.currentStore.StoreId, shippedStocks);
                foreach (Stock stock in shippedStocks)
                {
                    stock.StoreId = ReceivingStoreId;

                }
                await StoreDataStore.AddStocksAsync(ReceivingStoreId, shippedStocks);

            }));
        

        }
        public async Task ExecuteGetSerialFields()
        {

            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                IEnumerable<Product> products = await DataStore.GetProductsAsync();


                foreach (Stock stock in Stocks)
                {
                    Product product = products.FirstOrDefault(x => x.Id == stock.ProductId);

                    if (product.ProductType.ToLower() == "phone")
                    {
                        for (int i = 0; i < stock.Quantity; i++)
                        {
                            SerialNumbers.Add(new SerialNumber(stock.StoreId, stock.ProductId));
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                IsBusy = false;

            }


        }

    }
}

