using System;

using InventoryManager.Models;

namespace InventoryManager.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public bool IsPhone { get; set; }
        public bool IsAccessory { get; set; }
        public Product Product { get; set; }
        public ItemDetailViewModel(Product product = null)
        {
            Title = product?.Name;
            Product = product;
            IsPhone = Product.ProductType.ToLower() == "phone";
            IsAccessory = Product.ProductType.ToLower() == "accessory"; 
        }
    }
}
