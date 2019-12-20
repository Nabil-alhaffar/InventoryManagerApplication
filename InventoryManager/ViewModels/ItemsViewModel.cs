using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using InventoryManager.Models;
using InventoryManager.Views;

namespace InventoryManager.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Product> Products { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Product Manager";
            Products = new ObservableCollection<Product>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<AddNewProductPage, Product>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Product;
                Products.Add(newItem);
                await DataStore.AddProductAsync(newItem);
                
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Products.Clear();
                var items = await DataStore.GetProductsAsync(true);
                foreach (var item in items)
                {
                    Products.Add(item);
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