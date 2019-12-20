using System;
using System.Collections.Generic;

namespace InventoryManager.MobileAppService.Models3
{
    public partial class Orders
    {
        public Orders()
        {
            OrderItems = new HashSet<OrderItems>();
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int StoreId { get; set; }
        public int UserId { get; set; }

        public virtual Stores Store { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
