using System;
using System.Collections.Generic;

namespace InventoryManager.MobileAppService.Models3
{
    public partial class SerialNumbers
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public string SerialNumberValue { get; set; }

        public virtual Stocks Stocks { get; set; }
    }
}
