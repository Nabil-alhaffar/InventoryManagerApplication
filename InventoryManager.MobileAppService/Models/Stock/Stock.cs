using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InventoryManager.MobileAppService.Models
{
    public partial class Stock
    {
        public Stock()
        {

            SerialNumbers = new HashSet<SerialNumber>();
        }
        [Key]
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        [Key]
        [ForeignKey("StoreId")]
        public int StoreId { get; set; }
        [Required]
        public int Quantity { get; set; }

      //  public List<string> IMEInumbers { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }
        [JsonIgnore]
        public virtual Store Store { get; set; }
        public virtual ICollection<SerialNumber> SerialNumbers { get; set; }

    }
}
