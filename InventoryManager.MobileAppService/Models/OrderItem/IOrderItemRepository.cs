using System;
using System.Collections.Generic;

namespace InventoryManager.MobileAppService.Models
{
    public interface IOrderItemRepository
    {
        void Add(OrderItem order);
        void Update(OrderItem order);
        OrderItem Get(int orderId);
        IEnumerable<OrderItem> GetAll();
        OrderItem Remove(int key);
    }
}
