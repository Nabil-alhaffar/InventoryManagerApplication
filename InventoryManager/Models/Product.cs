using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace InventoryManager.Models
{
    public class Product
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
 //       public string Text { get; set; }
        public string ProductType { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public ProductType Category { get; set; }
        public decimal FullRetailPrice { get; set; }
        public decimal MinimumSetPrice { get; set; }
        public byte IsActive { get; set; }
        public string UPC { get; set; }

        //Phone Properties
        public string Manufacturer { get; set; }

        //Accessories Properties
        public string AccessoryItemType { get; set; }


    }
    public enum ProductType
    {
        [EnumMember(Value = "Phone")]
        Phone,
        [EnumMember(Value = "Accessory")]
        Accessory
    }
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AccessoryType {
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
        [EnumMember(Value ="N/A")]
        NotApplicable
    }
}
