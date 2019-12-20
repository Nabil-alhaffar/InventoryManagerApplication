using System;
using InventoryManager.Models;

namespace InventoryManager.ViewModels
{
    public class StocksDetailViewModel : BaseViewModel
    {
        public Stock Stock { get; set; }
        public StocksDetailViewModel(Stock stock = null)
        {
            Title = "Product Id: "+ stock?.ProductId.ToString()+ "Serial numbers";
            Stock = stock;

        }
    }
}

