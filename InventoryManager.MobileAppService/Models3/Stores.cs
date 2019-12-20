using System;
using System.Collections.Generic;

namespace InventoryManager.MobileAppService.Models3
{
    public partial class Stores
    {
        public Stores()
        {
            Orders = new HashSet<Orders>();
            Stocks = new HashSet<Stocks>();
            Users = new HashSet<Users>();
        }

        public string StoreName { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int StoreId { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Stocks> Stocks { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
