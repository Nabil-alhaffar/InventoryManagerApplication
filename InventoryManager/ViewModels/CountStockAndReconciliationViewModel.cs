using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using InventoryManager.Models;
using Xamarin.Forms;
using InventoryManager.Views;

namespace InventoryManager.ViewModels
{
    public class CountStockAndReconciliationViewModel : BaseViewModel
    {
        public ObservableCollection<Stock> Stocks { get; set; } = new ObservableCollection<Stock>();

        public CountStockAndReconciliationViewModel()
        {
            Title = "Add Stocks";
            Stocks = new ObservableCollection<Stock>();

            //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<CountStockAndReconciliationPage, IEnumerable<Stock>>(this, "PostNonSerialStocksCount",(async (obj, stocks) =>
            {
                var newstocks = stocks as IEnumerable<Stock>;
                await StoreDataStore.ReconcileInventoryAsync(App.currentStore.StoreId, newstocks);

            }));
        }
    }
}
