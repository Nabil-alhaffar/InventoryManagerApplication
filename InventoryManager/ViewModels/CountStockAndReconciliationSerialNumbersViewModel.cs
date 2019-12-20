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
    public class CountStockAndReconciliationSerialNumbersViewModel : BaseViewModel
    {
        public IEnumerable<Stock> Stocks { get; set; }
        public Command GetSerialFieldsCommand { get; set; }
        public ObservableCollection<SerialNumber> SerialNumbers { get; set; }

        public CountStockAndReconciliationSerialNumbersViewModel()
        {

            Title = "Add serial numbers to the count";
            Stocks = new List<Stock>();
            SerialNumbers = new ObservableCollection<SerialNumber>();
            GetSerialFieldsCommand = new Command(async () => await ExecuteGetSerialFields());
        }
        public CountStockAndReconciliationSerialNumbersViewModel(ObservableCollection<SerialNumber> serialNumbers)
        {
            SerialNumbers = new ObservableCollection<SerialNumber>();

            Title = "Add serial numbers to the count";
            foreach (SerialNumber imei in serialNumbers)
                SerialNumbers.Add(imei);
            MessagingCenter.Subscribe<CountStockAndReconciliationSerialNumbersPage, IEnumerable<Stock>>(this, "PostMixedStocksCount", (async (obj, stocks) =>
            {
                var newstocks = stocks as IEnumerable<Stock>;
                await StoreDataStore.ReconcileInventoryAsync(App.currentStore.StoreId, newstocks);

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

