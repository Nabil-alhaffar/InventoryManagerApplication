using System;
using InventoryManager.Models;

namespace InventoryManager.Models
{
    public class OrderItems
    {
        public OrderItems()
        {

        }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

    }
}
