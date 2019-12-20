using System;
using System.Collections.Generic;

namespace InventoryManager.MobileAppService.Models
{
    public interface IProductRepository
    {
        void Add(Product item);
        void Update(Product item);
        Product Remove(int key);
        Product Get(int id);
        IEnumerable<Product> GetAll();
    }
}
