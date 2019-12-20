using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using InventoryManager.Models;
using InventoryManager.Views;
using Xamarin.Forms;
using System.Linq;
namespace InventoryManager.ViewModels
{

    public class WelcomePageViewModel : BaseViewModel
    {

        public string StoreTotalStockCount { get; set; }
        public string StorePhoneStocksCount { get; set; }
        public string StoreAccessoryStocksCount { get; set; }

        public string StoreStocksTotalRetailValue { get; set; }
        public string StoreTotalStockCost { get; set; }
        public string StoreTotalPhoneStocksCost { get; set; }
        public string StoreTotalAccessoryStocksCost { get; set; }

        public IEnumerable<Stock> Stocks { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public string CurrentUserUsername { get; set; }
        public string CurrentStoreName { get; set; }

        public Command LoadElementsAsync { get; set; }


        public WelcomePageViewModel()
        { 
            CurrentUserUsername = App.currentUser.UserName;
            CurrentStoreName = App.currentStore.StoreName;
            LoadElementsAsync = new Command(async () => await ExecuteLoadElementsCommand());
            LoadElementsAsync.Execute(null);

        }

                public async Task ExecuteLoadElementsCommand() {
                    Products = await DataStore.GetProductsAsync();
                    decimal totalCostAccumulator = 0;
                    decimal totalRetailValueAccumulator = 0;

                    decimal phoneStocksCostAccumulator = 0;
                    decimal accessoryStocksCostAccumulator = 0;

                    int phoneStockCounter = 0;
                    int AccessoryStockCounter = 0;

                    Stocks = await StoreDataStore.GetStoreStocksAsync(App.currentStore.StoreId, true);

                    foreach (Stock stock in Stocks)
                    {
                        Product product = Products.FirstOrDefault(x => x.Id == stock.ProductId);
                        totalCostAccumulator += (product.Cost * stock.Quantity);
                        totalRetailValueAccumulator += (product.Cost * stock.Quantity);

                        if (product.ProductType.ToLower() == "phone")
                        {
                            phoneStockCounter += stock.Quantity;
                            phoneStocksCostAccumulator += (product.Cost * stock.Quantity);
                        }
                        else
                        {
                            AccessoryStockCounter += stock.Quantity;
                            accessoryStocksCostAccumulator += (product.Cost * stock.Quantity);

                        }

                    }

            StoreTotalStockCost = Stocks.Count().ToString();

            StorePhoneStocksCount = phoneStockCounter.ToString();
            StoreAccessoryStocksCount = AccessoryStockCounter.ToString();

            StoreStocksTotalRetailValue = totalRetailValueAccumulator.ToString();
            StoreTotalPhoneStocksCost = phoneStocksCostAccumulator.ToString();
            StoreTotalAccessoryStocksCost = accessoryStocksCostAccumulator.ToString();

        }

  }
        }
    



