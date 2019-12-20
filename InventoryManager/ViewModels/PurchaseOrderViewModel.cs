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
    public class PurchaseOrderViewModel :BaseViewModel
    {
        public ObservableCollection<Stock> Stocks { get; set; } = new ObservableCollection<Stock>();
        public Order Order { get; set; }
        public PurchaseOrderViewModel()
        {
            Title = "Purchase Order";
            Order = new Order { OrderDate = DateTime.Now, StoreId = App.currentStore.StoreId, UserId = App.currentUser.Id, OrderItems = new Collection<OrderItems>() }; 
            Stocks = new ObservableCollection<Stock>();
            MessagingCenter.Subscribe<PurchaseOrderPage, IEnumerable<Stock>>(this, "ReceivePurchaseOrder", (Action<PurchaseOrderPage, IEnumerable<Stock>>)(async (obj, stocks) =>
            {
                foreach (Stock stock in stocks){
                    Order.OrderItems.Add(new OrderItems { OrderId = Order.OrderId, ProductId = stock.ProductId, Quantity = stock.Quantity });

                }
                var newstocks = stocks as IEnumerable<Stock>;
                await StoreDataStore.AddStocksAsync(App.currentStore.StoreId, (IEnumerable<Stock>)newstocks);
                await OrderDataStore.AddOrderAync(Order);

            }));
        }
    }
}

