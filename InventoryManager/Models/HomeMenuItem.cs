using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManager.Models
{
    public enum MenuItemType
    {
        WelcomePage,
        ProductManager,
        Reconcile,
        CurrentStoreStocks,
        InventoryTransfers,
        PastOrders,
        About,


    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
