using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using InventoryManager.Models;
using Xamarin.Forms;

namespace InventoryManager.ViewModels
{
    public class ShipStocksViewModel : BaseViewModel
    {
        public ObservableCollection<Stock> Stocks { get; set; } = new ObservableCollection<Stock>();
        public int ReceivingStoreId { get; set; }
        public ShipStocksViewModel()
        {
            Title = "Transfer Stocks";
            Stocks = new ObservableCollection<Stock>();
            MessagingCenter.Subscribe<ShipStocksViewModel, IEnumerable<Stock>>(this, "ShipNonSerialStocks", (Action<ShipStocksViewModel, IEnumerable<Stock>>)(async (obj, stocks) =>
            {
                var shippedStocks = stocks as IEnumerable<Stock>;
                await StoreDataStore.SubtractStocksAsync (App.currentStore.StoreId, (IEnumerable<Stock>)shippedStocks);
                foreach (Stock stock in shippedStocks)
                {
                    stock.StoreId = ReceivingStoreId;

                }
                await StoreDataStore.AddStocksAsync(ReceivingStoreId, (IEnumerable<Stock>)shippedStocks);

            }));



        }
        

      
    }
}

