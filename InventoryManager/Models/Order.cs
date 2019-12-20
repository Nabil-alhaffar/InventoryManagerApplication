using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace InventoryManager.Models
{
    public class Order

    {
        public Order()
        {

        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int StoreId { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual Store Store { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
