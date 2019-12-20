using System;
using System.Collections.Generic;

namespace InventoryManager.MobileAppService.Models3
{
    public partial class Products
    {
        public Products()
        {
            Stocks = new HashSet<Stocks>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Category { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string Upc { get; set; }
        public byte IsActive { get; set; }
        public decimal MinimumSetPrice { get; set; }
        public decimal FullRetailPrice { get; set; }
        public string AccessoryItemType { get; set; }
        public string ProductType { get; set; }

        public virtual ICollection<Stocks> Stocks { get; set; }
    }
}
