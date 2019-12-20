using System;
using System.Collections.Generic;

namespace InventoryManager.MobileAppService.Models3
{
    public partial class OrderItems
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Orders Order { get; set; }
    }
}
