using System;
using InventoryManager.Models;
namespace InventoryManager.ViewModels
{
    public class PastOrdersDetailViewModel : BaseViewModel
    {
        public Order Order { get; set; }

        public PastOrdersDetailViewModel(Order order = null)
        {
             Title = "Order: " + Order?.OrderId.ToString();
            Order = order;
        

        }
    }
}

