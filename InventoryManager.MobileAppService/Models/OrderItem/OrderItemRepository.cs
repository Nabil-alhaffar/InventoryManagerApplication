using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

using System.Linq;
namespace InventoryManager.MobileAppService.Models
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private SqlDbContext dbContext;

        public OrderItemRepository(SqlDbContext _db)
        {
            dbContext = _db;

        }
        [Authorize]
        public IEnumerable<OrderItem> GetAll() => dbContext.OrderItems;

        public void Add(OrderItem orderItem)
        {
            //   order.OrderId = Guid.NewGuid().ToString();

            dbContext.OrderItems.Add(orderItem);
            dbContext.SaveChanges();
        }
        public OrderItem Get(int id) => dbContext.OrderItems.Find(id);

        public OrderItem Remove(int id)
        {
            OrderItem orderItem = dbContext.Find<OrderItem>(id);
            dbContext.OrderItems.Remove(orderItem);
            dbContext.SaveChanges();
            return orderItem;
        }
        public void Update(OrderItem orderItem)
        {
            OrderItem oldOrderItem = dbContext.Find<OrderItem>(orderItem.OrderId);
            dbContext.OrderItems.Update(orderItem);
            dbContext.SaveChanges();

        }
    }
}
