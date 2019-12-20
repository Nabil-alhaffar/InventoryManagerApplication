using System;
using System.Collections;
using System.Collections.Generic;
using InventoryManager.Models;
using System.ComponentModel.DataAnnotations;

namespace InventoryManager.MobileAppService.Models
{
    public partial class Store
    {
        public Store()
        {
           // SerialNumbers = new HashSet<SerialNumber>();
            Orders = new HashSet<Order>();
            Stocks = new HashSet<Stock>();
            Users = new HashSet<User>();
        }
        [Key]
        public int StoreId { get; set; }
        [Required]
        public string StoreName { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }
       // public virtual ICollection<SerialNumber> SerialNumbers { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
