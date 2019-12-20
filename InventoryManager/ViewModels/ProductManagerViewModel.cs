using System;
namespace InventoryManager.ViewModels
{
    public class ProductManagerViewModel
    {
        public enum ItemType { PhoneItem = 0, AccessoryItem = 1 }
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string Vendor { get; set; }
        public string Manufacturer { get; set; }
        public string Cost { get; set; }
        public ItemType Category { get; set; }

        public ProductManagerViewModel()
        {
             
        }
    }
}
