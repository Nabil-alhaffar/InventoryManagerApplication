using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InventoryManager.MobileAppService.Models
{
    public partial class Order
    {
        public Order()
        {
            //OrderItems = new HashSet<OrderItems>();
            OrderItems = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int StoreId { get; set; }
        public int UserId { get; set; }


        [JsonIgnore]
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual Store Store { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        //public virtual ICollection<OrderItem> Stocks { get; set; }

    }
}
