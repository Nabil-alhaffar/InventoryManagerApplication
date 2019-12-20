using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InventoryManager.MobileAppService.Models
{
    public partial class OrderItem
    {
        public int OrderId { get; set; }
        //public int ItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        //public decimal ListPrice { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; }
        //public virtual Product Product { get; set; }
    }
}
