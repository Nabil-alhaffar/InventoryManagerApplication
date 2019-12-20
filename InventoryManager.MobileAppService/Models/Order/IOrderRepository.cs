using System;
using System.Collections.Generic;

namespace InventoryManager.MobileAppService.Models
{
    public interface IOrderRepository
    {
        void Add(Order order);
        void Update(Order order);
        Order Get(int orderId);
        IEnumerable<Order> GetAll();
        Order Remove(int key);
    }
}



