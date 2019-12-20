using System;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using InventoryManager.Models;
using InventoryManager.Views;
namespace InventoryManager.ViewModels
{
    public class PastOrdersViewModel:BaseViewModel
    {
        public ObservableCollection<Order> Orders { get; set; }
        public Command LoadOrdersCommand { get; set; }
        public PastOrdersViewModel()
        {
            Title = "Past Orders";
            Orders = new ObservableCollection<Order>();
            LoadOrdersCommand = new Command(async () => await ExecuteLoadOrdersCommand());

        }
        async Task ExecuteLoadOrdersCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Orders.Clear();
                var orders = await OrderDataStore.GetOrdersAsync(true);
                foreach (Order item in orders)
                {
                    Orders.Add(item);
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

