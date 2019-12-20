using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace InventoryManager.MobileAppService.Models
{
    public partial class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ProductType { get; set; }
        public ProductType Category { get; set; }
        [Required]
        public float Cost { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal FullRetailPrice { get; set; }
        [Required]
        public byte IsActive { get; set; }
        [Required]
        public decimal MinimumSetPrice { get; set; }
        [Required]
        public string UPC { get; set; }


        public string Manufacturer { get; set; }

        //Accessories Properties
        public string AccessoryItemType { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
    //[JsonConverter(typeof(StringEnumConverter))]
    public enum ProductType {
        [EnumMember(Value = "Phone")]
        Phone ,
        [EnumMember(Value = "Accessory")]
        Accessory
    }


    //[JsonConverter(typeof(StringEnumConverter))]
    public enum AccessoryType 
    {
        [EnumMember(Value = "Protective Case")]
        ProtectiveCase,
        [EnumMember(Value = "Screen Protector")]
        ScreenProtector,
        [EnumMember(Value = "Storage")]
        Storage,
        [EnumMember(Value = "Bluetooth Speakers")]
        Speakers,
        [EnumMember(Value = "Bluetooth Headphones")]
        BluetoothHeadphones,
        [EnumMember(Value = "Earbuds")]
        Earbuds,
        [EnumMember(Value = "Power Banks")]
        PowerBank,
        [EnumMember(Value = "Charging Cables")]
        ChargingCables,
        [EnumMember(Value = "Miscellaneous")]
        Miscellaneous,
        [EnumMember(Value = "N/A")]
        NotApplicable
    }
}
