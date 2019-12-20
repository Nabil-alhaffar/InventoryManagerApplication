using System;
using System.Collections.Generic;

namespace InventoryManager.MobileAppService.Models3
{
    public partial class Stocks
    {
        public Stocks()
        {
            SerialNumbers = new HashSet<SerialNumbers>();
        }

        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Products Product { get; set; }
        public virtual Stores Store { get; set; }
        public virtual ICollection<SerialNumbers> SerialNumbers { get; set; }
    }
}
