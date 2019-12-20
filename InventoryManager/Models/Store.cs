using System;
using System.Collections;
using System.Collections.Generic;

namespace InventoryManager.Models
{
    public class Store
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }


        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}