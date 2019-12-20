using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
namespace InventoryManager.Models
{
    public class Stock
    {

        [Key]
        public int ProductId { get; set; }
        [Required]
        public int StoreId { get; set; }
        [Required]
        public int Quantity { get; set; }

        [JsonIgnore]
        public virtual Product Product { get; set; }
        [JsonIgnore]
        public virtual Store Store { get; set; }
        public virtual ICollection<SerialNumber> SerialNumbers { get; set; }

        public Stock() { }
        public Stock(int storeId) {

            StoreId = storeId;
        }
    }
}
