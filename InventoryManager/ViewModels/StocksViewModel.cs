using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;

using InventoryManager.Models;
using InventoryManager.Views;
using System.Collections.Generic;

namespace InventoryManager.ViewModels
{
    public class StocksViewModel : BaseViewModel
    {
        public ObservableCollection<Stock> Stocks { get; set; }
        public Command LoadStocksCommand { get; set; }
        public StocksViewModel()
        {
            Title = "Current Store Stocks";
            Stocks = new ObservableCollection<Stock>();
            LoadStocksCommand = new Command(async () => await ExecuteLoadStocksCommand());

            MessagingCenter.Subscribe<IEnumerable<Stock>>(this, "AddStocks", (Action<IEnumerable<Stock>>)(async (stocks) =>
            {
                int storeId = stocks.First().StoreId;
                foreach (Stock stock in stocks)
                {
                    var newStock = stock as Stock;
                    Stocks.Add(stock);
                }
                await StoreDataStore.AddStocksAsync(storeId, stocks);

            }));

        }
        async Task ExecuteLoadStocksCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                 Stocks.Clear();
                var stocks = await StoreDataStore.GetStoreStocksAsync(App.currentStore.StoreId,true);
                foreach (var stock in stocks)
                {
                    Stocks.Add(stock);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

