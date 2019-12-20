using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InventoryManager.MobileAppService.Models
{
    public class SerialNumber
    {
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        [ForeignKey("StoreId")]
        public int StoreId { get; set; }
        [Key]
        public string SerialNumberValue { get; set; }
        [JsonIgnore]
        public virtual Stock Stock { get; set; }

    }
}
