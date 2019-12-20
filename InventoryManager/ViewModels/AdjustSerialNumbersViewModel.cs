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
    public class AdjustSerialNumbersViewModel: BaseViewModel
    {
        public IEnumerable<Stock> Stocks { get; set; } 
        public Command GetSerialFieldsCommand { get; set; }
        public ObservableCollection<SerialNumber> SerialNumbers { get; set; }
        public AdjustSerialNumbersViewModel()
        {

            Title = "List Serial Numbers";
            Stocks = new List<Stock>();
            SerialNumbers = new ObservableCollection<SerialNumber>();
            GetSerialFieldsCommand = new Command(async () => await ExecuteGetSerialFields());


        }
        public AdjustSerialNumbersViewModel(ObservableCollection<SerialNumber> serialNumbers)
        {
            SerialNumbers = new ObservableCollection<SerialNumber>();

        Title = "List Serial Numbers";
            foreach (SerialNumber imei in serialNumbers)
                SerialNumbers.Add(imei);
            MessagingCenter.Subscribe<AddSerialNumbersPage, IEnumerable<Stock>>(this, "AddMixedStocks", (async (obj, stocks) =>
            {
                var newstocks = stocks as IEnumerable<Stock>;
                await StoreDataStore.AddStocksAsync(App.currentStore.StoreId, newstocks);

            }));
            MessagingCenter.Subscribe<SubtractSerialNumbersPage, IEnumerable<Stock>>(this, "SubtractMixedStocks", (async (obj, stocks) =>
            {
                var newstocks = stocks as IEnumerable<Stock>;
                await StoreDataStore.SubtractStocksAsync(App.currentStore.StoreId, newstocks);

            }));

            MessagingCenter.Subscribe<PurchaseOrderSerialNumberPage, IEnumerable<Stock>>(this, "ReceiveMixedPurchaseOrder",(async (obj, stocks) =>
            {
                DateTime currentDateTime = DateTime.Now;

                Order order = new Order { OrderDate = currentDateTime, StoreId = App.currentStore.StoreId, UserId = App.currentUser.Id, OrderItems = new Collection<OrderItems>() };

                foreach (Stock stock in stocks)
                {
                    order.OrderItems.Add(new OrderItems { OrderId = order.OrderId, ProductId = stock.ProductId, Quantity = stock.Quantity });

                }
                var newstocks = stocks as IEnumerable<Stock>;
                await StoreDataStore.AddStocksAsync(App.currentStore.StoreId, newstocks);
                await OrderDataStore.AddOrderAync(order);

            }));


            //MessagingCenter.Subscribe<AddSerialNumbersPage, IEnumerable<Stock>>(this, "AddSerialNumbers", async (obj, stocks) =>
            //{
            //    var newSerialNumbers = serialNumbers as IEnumerable<SerialNumber>;
            //    await Serial.AddStocksAsync(App.currentStore.StoreId, newstocks);

            //});


            //GetSerialFieldsCommand = new Command(async () => await ExecuteGetSerialFields());
            //MessagingCenter.Subscribe<AddnewSerialPage, Product>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Product;
            //    Products.Add(newItem);
            //    await DataStore.AddProductAsync(newItem);

            //});

        }

        public  async Task ExecuteGetSerialFields ()
        {

                if (IsBusy)
                    return ;

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
            finally {
                IsBusy = false;

            }
            

        }
    }
}